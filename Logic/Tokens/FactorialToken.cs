using Logic.Interfaces;

namespace Logic.Tokens
{
    public class FactorialToken : IToken
    {
        public IToken Item1 { get; }
        
        public FactorialToken(IToken item1)
        {
            Item1 = item1;
        }

        public override string ToString()
        {
            return $"{Item1}!";
        }
    }
}