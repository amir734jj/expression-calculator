using System;
using System.Linq;
using System.Linq.Expressions;

namespace Logic
{
    public class Driver
    {
        private readonly Semantic _semantic;
        
        private readonly Parser _parser;
        
        private readonly CodeGen _codeGen;

        public Driver()
        {
            _parser = new Parser();
            _semantic = new Semantic();
            _codeGen = new CodeGen();
        }
        
        public string Run(string code)
        {
            var tokens = _parser.Parse(code);
            var semanticPayload = _semantic.Visit(tokens);
            
            if (semanticPayload.Error.Messages.Any())
            {
                throw new Exception(string.Join(Environment.NewLine, semanticPayload.Error.Messages));
            }
            
            var codeGen = _codeGen.Visit(tokens);

            var lambdaExpr = Expression.Lambda(codeGen.Expression);

            return lambdaExpr.Compile().DynamicInvoke()?.ToString();
        }
    }
}