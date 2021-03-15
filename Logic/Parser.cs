using System.Collections.Generic;
using System.Linq;
using FParsec;
using FParsec.CSharp;
using Microsoft.FSharp.Core;
using Logic.Interfaces;
using Logic.Tokens;
using Microsoft.FSharp.Collections; // extension functions (combinators & helpers)
using static FParsec.CSharp.PrimitivesCS; // combinator functions
using static FParsec.CSharp.CharParsersCS;

namespace Logic
{
    public class Parser : IParser
    {
        private readonly FSharpFunc<CharStream<Unit>, Reply<FSharpList<IToken>>> _parser;

        public Parser()
        {
            var variableP = Regex(@"\w+").Label("variable")
                .Map(x => (IToken) new VariableToken(x));
            var numberP = Float.Label("number").Map(x => (IToken) new NumberToken(x));
            var atomicP = numberP.Or(variableP);

            FSharpFunc<CharStream<Unit>, Reply<IToken>> exprP = null;
            
            var assignmentP = variableP.AndLTry(WS).AndLTry(StringP("=")).AndL(WS).AndTry(Rec(() => exprP)).AndL(WS)
                .Map((x, y) => (IToken) new AssignmentToken(((VariableToken) x).Item1, y));

            var nonRecursiveExprP = WS.And(assignmentP.Or(atomicP)).And(WS);

            var operatorP = new OPPBuilder<Unit, IToken, Unit>()
                .WithOperators(ops => ops
                    .AddInfix("<", 5, WS, (x, y) => new BooleanLessThan(x, y))
                    .AddInfix("<", 5, WS, (x, y) => new BooleanLessThan(x, y))
                    .AddInfix("<", 5, WS, (x, y) => new BooleanLessThan(x, y))
                    .AddInfix("<", 5, WS, (x, y) => new BooleanLessThan(x, y))
                    .AddInfix("+", 10, WS, (x, y) => new AddToken(x, y))
                    .AddInfix("-", 10, WS, (x, y) => new DivideToken(x, y))
                    .AddInfix("*", 20, WS, (x, y) => new MultiplyToken(x, y))
                    .AddInfix("/", 20, WS, (x, y) => new DivideToken(x, y))
                    .AddPrefix("-", 20, x => new NegateToken(x))
                    .AddInfix("^", 30, Associativity.Right, WS, (x, y) => new PowerToken(x, y))
                    .AddPostfix("!", 40, x => new FactorialToken(x))
                    .AddTernary("?", ":", 50, Associativity.None,
                        (condT, ifT, elseT) => new ConditionalToken(condT, ifT, elseT))
                )
                .WithImplicitOperator(20, (x, y) => new MultiplyToken(x, y))
                .WithTerms(term => Choice(nonRecursiveExprP, Between(CharP('(').And(WS), term, CharP(')').And(WS))))
                .Build()
                .ExpressionParser
                .Label("expression");

            exprP = assignmentP.Or(operatorP).Or(atomicP);
            
            _parser = Many(exprP, sep: WS.And(CharP(';')).And(WS), canEndWithSep: true);
        }

        public List<IToken> Parse(string str)
        {
            var re = _parser.ParseString(str);

            return re.Result.ToList();
        }
    }
}