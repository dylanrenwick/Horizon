namespace Horizon.Tokenizing;

internal class CharTokenParser : TokenParser
{
    private readonly char c;

    public CharTokenParser(TokenType type, char c)
        : base(type)
    {
        this.c = c;
    }

    public override bool Check(string s)
    {
        return s.Length == 1 && s[0] == c;
    }
}
