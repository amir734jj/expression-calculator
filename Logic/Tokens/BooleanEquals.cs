using Logic.Interfaces;

namespace Logic.Tokens
{
    public class BooleanEquals : IToken
    {
        public IToken Item1 { get; }
        
        public IToken Item2 { get; }

        public BooleanEquals(IToken item1, IToken item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override string ToString()
        {
            return "";
        }
    }
}