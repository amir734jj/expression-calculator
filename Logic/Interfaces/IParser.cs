using System.Collections.Generic;

namespace Logic.Interfaces
{
    public interface IParser
    {
        List<IToken> Parse(string str);
    }
}