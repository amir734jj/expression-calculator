using System;
using System.Linq.Expressions;

namespace Logic
{
    public class Driver
    {
        public Driver(string code)
        {
            var tokens = new Parser().Parse(code);
            var semanticPayload = new Semantic().Visit(tokens);
            
            foreach (var errorMessage in semanticPayload.Error.Messages)
            {
                Console.WriteLine(errorMessage);
            }

            var codeGen = new CodeGen().Visit(tokens);

            var lambdaExpr = Expression.Lambda(codeGen.Expression);
            
            Console.WriteLine(lambdaExpr.Compile().DynamicInvoke());
        }
    }
}