using Logic.Interfaces;

namespace Logic.Tokens
{
    public class BooleanNot : IToken
    {
        public IToken Item1 { get; }

        public BooleanNot(IToken item1)
        {
            Item1 = item1;
        }

        public override string ToString()
        {
            return $"!${Item1}";
        }
    }
}