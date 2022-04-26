using System.Text;

using Horizon.Logging;

namespace Horizon.Tokenizing;

internal class Tokenizer
{
    private readonly Logger log;

    private string sourceCode;

    private int sourcePos;
    private int sourceLine;
    private int sourceColumn;

    private readonly StringBuilder tokenBuilder = new();
    private readonly List<Token> tokens = new();

    public Tokenizer(Logger logger)
    {
        log = logger;
    }

    public IEnumerable<Token> Tokenize(string source)
    {
        sourceCode = source;
        ParseTokens();
        return tokens;
    }

    private void ParseTokens()
    {
        sourcePos = 0;
        sourceLine = 0;
        sourceColumn = 0;

        tokens.Clear();

        Token next;
        do
        {
            next = ParseNextToken();
            AddToken(next);

            log.Debug(next.ToString());
        } while (next.Type != TokenType.EOF);
    }

    private Token ParseNextToken()
    {
        if (sourcePos >= sourceCode.Length) return Token.EOF(sourcePos, sourceLine, sourceColumn);

        TokenType lastType = TokenType.None;
        for (
            tokenBuilder.Clear();
            sourcePos < sourceCode.Length;
            sourcePos++
        )
        {
            tokenBuilder.Append(sourceCode[sourcePos]);
            TokenType? tokenType = TokenParser.TryParseToken(tokenBuilder.ToString());
            if (tokenType.HasValue)
            {
                lastType = tokenType.Value;
            }
            else
            {
                if (lastType == TokenType.None)
                    throw new TokenizerException($"Untokenizable string '{tokenBuilder}'");

                tokenBuilder.Remove(tokenBuilder.Length - 1, 1);
                return BuildToken(tokenBuilder.ToString(), lastType);
            }
        }

        if (tokenBuilder.Length > 0)
        {
            if (lastType == TokenType.None)
                throw new TokenizerException($"Untokenizable string {tokenBuilder}");

            return BuildToken(tokenBuilder.ToString(), lastType);
        }
        else
        {
            return Token.EOF(sourcePos, sourceLine, sourceColumn);
        }
    }

    private void AddToken(Token token)
    {
        tokens.Add(token);
        sourceColumn += token.Length;
        if (token.Type == TokenType.Newline)
        {
            sourceLine++;
            sourceColumn = 0;
        }
    }

    private Token BuildToken(string val, TokenType type)
    {
        return new(
            val,
            type,
            sourcePos, val.Length,
            sourceLine, sourceColumn
        );
    }
}

public class TokenizerException : Exception
{
    public TokenizerException(string message)
        : base(message) { }
}
