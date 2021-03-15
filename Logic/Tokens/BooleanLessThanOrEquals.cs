using Logic.Interfaces;

namespace Logic.Tokens
{
    public class BooleanLessOrEqualsToken : IToken
    {
        public IToken Item1 { get; }
        
        public IToken Item2 { get; }

        public BooleanLessOrEqualsToken(IToken item1, IToken item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}