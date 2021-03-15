using Logic.Interfaces;

namespace Logic.Tokens
{
    public class BooleanLessThan : IToken
    {
        public IToken Item1 { get; }
        
        public IToken Item2 { get; }

        public BooleanLessThan(IToken item1, IToken item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}