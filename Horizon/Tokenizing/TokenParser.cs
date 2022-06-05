using System.Text.RegularExpressions;

namespace Horizon.Tokenizing;

internal abstract class TokenParser
{

    public readonly TokenType Type;

    public TokenParser(TokenType type)
    {
        Type = type;
    }

    public abstract bool Check(string s);

    public virtual Token Parse(string s, int pos, int len, int line, int col)
    {
        return new(s, Type, pos, len, line, col);
    }
}
