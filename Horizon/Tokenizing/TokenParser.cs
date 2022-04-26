using System.Text.RegularExpressions;

namespace Horizon.Tokenizing;

internal class TokenParser
{
    public readonly Func<string, bool> Parse;
    public readonly TokenType Type;

    private TokenParser(TokenType type, Func<string, bool> parse)
    {
        Parse = parse;
        Type = type;
    }

    private static TokenParser Regex(TokenType type, string regex) => new(type, s => new Regex($"^{regex}$").IsMatch(s));
    private static TokenParser Char(TokenType type, char ch) => new(type, s => s.Length == 1 && s[0] == ch);

    private static readonly List<TokenParser> tokenParsers = new()
    {
        Regex(TokenType.Newline, @"[\r\n]+"),
        Regex(TokenType.Whitespace, @"[ \t]+"),
        Regex(TokenType.Float, @"((\d+\.\d+)|(\d+\.)|(\.\d+))"),
        Regex(TokenType.Integer, @"\d+"),
        Char(TokenType.Add, '+'),
        Char(TokenType.Subtract, '-'),
        Char(TokenType.Multiply, '*'),
        Char(TokenType.Divide, '/'),
        Char(TokenType.Modulus, '%'),
        Char(TokenType.MemberAccess, '.'),
        new(TokenType.String, ParseString)
    };

    public static TokenType? TryParseToken(string token)
    {
        foreach (var parser in tokenParsers)
        {
            if (parser.Parse(token)) return parser.Type;
        }

        return null;
    }

    private static bool ParseString(string s)
    {

    }
}
