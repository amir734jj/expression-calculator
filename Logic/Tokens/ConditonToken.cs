using Logic.Interfaces;

namespace Logic.Tokens
{
    public class ConditionalToken : IToken
    {
        public IToken Item1 { get; }
        public IToken Item2 { get; }
        public IToken Item3 { get; }

        public ConditionalToken(IToken item1, IToken item2, IToken item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public override string ToString()
        {
            return $"{Item1} ? {Item2} : {Item3}";
        }
    }
}