using Logic.Interfaces;

namespace Logic.Tokens
{
    public class VariableToken : IToken
    {
        public string Item1 { get; }

        public VariableToken(string item1)
        {
            Item1 = item1;
        }

        public override string ToString()
        {
            return Item1;
        }
    }
}