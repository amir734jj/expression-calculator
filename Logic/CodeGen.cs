using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Logic.Abstracts;
using Logic.Interfaces;
using Logic.Models;
using Logic.Tokens;

namespace Logic
{
    public class CodeGenPayload : ICombinable<CodeGenPayload>
    {
        public Expression Expression { get; set; } = Expression.Empty();

        public Contour<CodeGenPayload> Contour = new Contour<CodeGenPayload>();

        public CodeGenPayload Combine(CodeGenPayload other)
        {
            return new CodeGenPayload
            {
                Expression = other.Expression,
                Contour = Contour.Combine(other.Contour)
            };
        }
    }
    
    public class CodeGen : Visitor<CodeGenPayload>
    {
        public override CodeGenPayload Visit(AddToken token, CodeGenPayload state = default)
        {
            return new CodeGenPayload
            {
                Expression = Expression.Add(Visit(token.Item1, state).Expression, Visit(token.Item2, state).Expression)
            };
        }

        public override CodeGenPayload Visit(SubtractToken token, CodeGenPayload state = default)
        {
            return new CodeGenPayload
            {
                Expression = Expression.Subtract(Visit(token.Item1, state).Expression, Visit(token.Item2, state).Expression)
            };
        }

        public override CodeGenPayload Visit(MultiplyToken token, CodeGenPayload state = default)
        {
            var r = Visit(token.Item2, state);
            
            return new CodeGenPayload
            {
                Expression = Expression.Multiply(Visit(token.Item1, state).Expression, r.Expression)
            };
        }

        public override CodeGenPayload Visit(DivideToken token, CodeGenPayload state = default)
        {
            return new CodeGenPayload
            {
                Expression = Expression.Divide(Visit(token.Item1, state).Expression, Visit(token.Item2, state).Expression)
            };
        }

        public override CodeGenPayload Visit(NegateToken token, CodeGenPayload state = default)
        {
            return new CodeGenPayload
            {
                Expression = Expression.Negate(Visit(token.Item1, state).Expression)
            };
        }

        public override CodeGenPayload Visit(FactorialToken token, CodeGenPayload state = default)
        {
            var factorialPExpr = Expression.Parameter(typeof(double));
            var factorialExpr = Expression.Lambda(Expression.Invoke(Expression.Constant((Func<double, double>) Factorial), factorialPExpr), factorialPExpr);
            return state?.Combine(new CodeGenPayload {Expression = Expression.Invoke(factorialExpr, Visit(token, state).Expression)});
        }

        public override CodeGenPayload Visit(PowerToken token, CodeGenPayload state = default)
        {
            var powExpr = Expression.Power(Visit(token.Item1, state).Expression, Visit(token.Item2, state).Expression);
            return state?.Combine(new CodeGenPayload {Expression = powExpr});
        }

        public override CodeGenPayload Visit(AssignmentToken token, CodeGenPayload state = default)
        {
            return state?.Combine(new CodeGenPayload {Contour = new Contour<CodeGenPayload>(token.Item1, Visit(token.Item2, state))});
        }

        public override CodeGenPayload Visit(VariableToken token, CodeGenPayload state = default)
        {
            return state?.Combine(state.Contour[token.Item1]);
        }

        public override CodeGenPayload Visit(NumberToken token, CodeGenPayload state = default)
        {
            return state?.Combine(new CodeGenPayload {Expression = Expression.Constant(token.Item1)});
        }
        
        private static double Factorial(double n)
        {
            var j = 1;
            for (var i = 1; i <= n; i++)
            {
                j *= i;
            }

            return j;
        }
    }
}