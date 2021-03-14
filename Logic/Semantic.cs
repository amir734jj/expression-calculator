using System;
using Logic.Abstracts;
using Logic.Interfaces;
using Logic.Models;
using Logic.Tokens;

namespace Logic
{
    public class SemanticPayload : ICombinable<SemanticPayload>
    {
        public Error Error { get; set; } = new Error();
        
        public Contour<None> Contour = new Contour<None>();

        public SemanticPayload Combine(SemanticPayload other)
        {
            return new SemanticPayload
            {
                Error = Error.Combine(other.Error),
                Contour = Contour.Combine(other.Contour)
            };
        }
    }

    public class Semantic : Visitor<SemanticPayload>
    {
        public override SemanticPayload Visit(AssignmentToken token, SemanticPayload payload)
        {
            return Visit(token.Item2, payload)
                .Combine(new SemanticPayload {Contour = new Contour<None>(token.Item1, None.New())});
        }

        public override SemanticPayload Visit(VariableToken token, SemanticPayload payload)
        {
            if (!payload.Contour.Lookup(token.Item1, out _))
            {
                payload.Error = payload.Error.Combine(new Error($"Parameter {token.Item1} is unbound"));
            }

            return payload;
        }
    }
}