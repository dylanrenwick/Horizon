using System.Text.RegularExpressions;

namespace Horizon.Tokenizing;

internal class RegexTokenParser : TokenParser
{
    private readonly Regex regex;

    public RegexTokenParser(TokenType type, string regex)
        : this(type, new Regex($"^{regex}$", RegexOptions.None)) { }
    public RegexTokenParser(TokenType type, Regex regex)
        : base(type)
    {
        this.regex = regex;
    }

    public override bool Check(string s)
    {
        return regex.Match(s).Value == s;
    }
}
