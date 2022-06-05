namespace Horizon.Tokenizing;

internal class KeywordTokenParser : TokenParser
{
    private readonly string keyword;

    public KeywordTokenParser(TokenType type, string keyword)
        : base(type)
    {
        this.keyword = keyword;
    }

    public override bool Check(string s)
    {
        return keyword.StartsWith(s);
    }
}
