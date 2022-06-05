using Horizon.Logging;
using Horizon.Parsing;
using Horizon.Tokenizing;

namespace Horizon;

public class HorizonCompiler
{
    private readonly Logger log;

    private readonly Tokenizer tokenizer;
    private readonly Parser parser;
    //private readonly IPlatformDriver platform;

    public HorizonCompiler(Logger logger)
    {
        log = logger;
        tokenizer = new Tokenizer(log.Label("TKN"), GetTokenParsers());
        parser = new Parser(log.Label("PRS"));
    }

    public void Compile(string sourceCode)
    {
        log.Info("Beginning Tokenization");
        var tokens = tokenizer.Tokenize(sourceCode);
        log.Info($"Loaded {tokens.Length} tokens");
    }

    private List<TokenParser> GetTokenParsers()
    {
        return new()
        {
            new CharTokenParser(TokenType.Newline, '\n'),
            new RegexTokenParser(TokenType.Whitespace, @"[ \t]+"),

            new RegexTokenParser(TokenType.Float, @"((\d+\.\d+)|(\d+\.)|(\.\d+))"),
            new RegexTokenParser(TokenType.Integer, @"\d+"),

            new CharTokenParser(TokenType.OpenBrace, '{'),
            new CharTokenParser(TokenType.CloseBrace, '}'),
            new CharTokenParser(TokenType.OpenBracket, '['),
            new CharTokenParser(TokenType.CloseBracket, ']'),
            new CharTokenParser(TokenType.OpenParen, '('),
            new CharTokenParser(TokenType.CloseParen, ')'),
            new CharTokenParser(TokenType.Colon, ':'),

            new CharTokenParser(TokenType.Add, '+'),
            new CharTokenParser(TokenType.Subtract, '-'),
            new CharTokenParser(TokenType.Multiply, '*'),
            new CharTokenParser(TokenType.Divide, '/'),
            new CharTokenParser(TokenType.Modulus, '%'),
            new CharTokenParser(TokenType.MemberAccess, '.'),
            new ExactTokenParser(TokenType.PipeRight, "->"),
            new ExactTokenParser(TokenType.PipeLeft, "<-"),

            new KeywordTokenParser(TokenType.IntKeyword, "int"),
            new KeywordTokenParser(TokenType.BoolKeyword, "bool"),
            new KeywordTokenParser(TokenType.CharKeyword, "char"),
            new RegexTokenParser(TokenType.Identifier, @"[_a-zA-Z][_a-zA-Z0-9]*"),
            new StringTokenParser(TokenType.String)
        };
    }
}