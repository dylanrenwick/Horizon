namespace Horizon.Tokenizing;

internal class Token
{
    public readonly string Value;
    public readonly TokenType Type;

    public readonly int StartPos;
    public readonly int Length;

    public readonly int Line;
    public readonly int Column;

    public Token(string value, TokenType type, int pos, int len, int line, int col)
    {
        Value = value;
        Type = type;

        StartPos = pos;
        Length = len;

        Line = line;
        Column = col;
    }

    public override string ToString()
    {
        return $"{{{Type} Line {Line}, Col {Column}}}: '{SanitizedValue}'";
    }

    private string SanitizedValue => Value.Replace("\r", "\\r").Replace("\n", "\\n");

    public static Token EOF(int pos, int line, int col)
    {
        return new(
            string.Empty,
            TokenType.EOF,
            pos, 0,
            line, col
        );
    }

    public static Token None(int pos, int line, int col)
    {
        return new(
            string.Empty,
            TokenType.None,
            pos, 0,
            line, col
        );
    }
}
