using System.Globalization;
using Logic.Interfaces;

namespace Logic.Tokens
{
    public class NumberToken : IToken
    {
        public double Item1 { get; }

        public NumberToken(double item1)
        {
            Item1 = item1;
        }

        public override string ToString()
        {
            return Item1.ToString(CultureInfo.InvariantCulture);
        }
    }
}