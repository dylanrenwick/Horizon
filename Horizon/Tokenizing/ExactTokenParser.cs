namespace Horizon.Tokenizing;

internal class ExactTokenParser : TokenParser
{
    private readonly string exactMatch;

    public ExactTokenParser(TokenType type, string value)
        : base(type)
    {
        exactMatch = value;
    }

    public override bool Check(string s)
    {
        return s == exactMatch;
    }
}
