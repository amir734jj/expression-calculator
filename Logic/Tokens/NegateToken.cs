using Logic.Interfaces;

namespace Logic.Tokens
{
    public class NegateToken : IToken
    {
        public IToken Item1 { get; }
        
        public NegateToken(IToken item1)
        {
            Item1 = item1;
        }

        public override string ToString()
        {
            return $"- {Item1}";
        }
    }
}