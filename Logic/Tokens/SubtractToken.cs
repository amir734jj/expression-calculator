using Logic.Interfaces;

namespace Logic.Tokens
{
    public class SubtractToken : IToken
    {
        public IToken Item1 { get; }
        public IToken Item2 { get; }

        public SubtractToken(IToken item1, IToken item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override string ToString()
        {
            return $"{Item1} - {Item2}";
        }
    }
}