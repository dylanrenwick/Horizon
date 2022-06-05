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

    private readonly List<TokenParser> tokenParsers;

    public Tokenizer(Logger logger, List<TokenParser> parsers)
    {
        log = logger;
        tokenParsers = parsers;
    }

    public TokenStream Tokenize(string source)
    {
        sourceCode = source;
        ParseTokens();
        return new TokenStream(tokens);
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

        TokenParser? tokenParser = null;
        for (
            tokenBuilder.Clear();
            sourcePos < sourceCode.Length;
            sourcePos++
        )
        {
            char nextChar = sourceCode[sourcePos];
            if (tokenBuilder.Length > 0)
            {
                if (tokenParser == null) tokenParser = TryGetTokenParser(tokenBuilder.ToString());
                if (tokenParser == null) throw new TokenizerException($"Untokenizable string {tokenBuilder}");

                if (!tokenParser.Check(tokenBuilder.ToString() + nextChar))
                {
                    var nextTokenParser = TryGetTokenParser(tokenBuilder.ToString() + nextChar);
                    if (nextTokenParser == null) break;

                    tokenParser = nextTokenParser;
                }
            }

            tokenBuilder.Append(nextChar);
        }

        if (tokenBuilder.Length > 0)
        {
            if (tokenParser == null)
                throw new TokenizerException($"Untokenizable string {tokenBuilder}");

            return tokenParser.Parse(
                tokenBuilder.ToString(),
                sourcePos - (tokenBuilder.Length + 1),
                tokenBuilder.Length,
                sourceLine, sourceColumn
            );
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

    private TokenParser? TryGetTokenParser(string token)
    {
        foreach (var parser in tokenParsers)
        {
            if (parser.Check(token)) return parser;
        }

        return null;
    }
}

public class TokenizerException : Exception
{
    public TokenizerException(string message)
        : base(message) { }
}
