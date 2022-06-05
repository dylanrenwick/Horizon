using Horizon.Logging;
using Horizon.Parsing.AST;
using Horizon.Tokenizing;

namespace Horizon.Parsing;

internal class Parser
{
    private readonly Logger log;
    private readonly NameResolver nameResolver;

    private TokenStream tokens;

    public Parser(Logger logger)
    {
        log = logger;

        nameResolver = new();
    }

    public ProgramASTNode Parse(TokenStream tokens)
    {
        this.tokens = tokens;
    }

    private List<FunctionASTNode> ParseFunctions()
    {
        IEnumerable<Footprint> footprints = ParseFootprints();
        nameResolver.RegisterFunctions(footprints);

    }

    private FunctionASTNode ParseFunction()
    {
        throw new NotImplementedException();
    }

    private IEnumerable<Footprint> ParseFootprints()
    {
        return tokens.FromEach(TokenType.FuncKeyword, ParseFootprint);
    }

    private Footprint ParseFootprint()
    {
        string name = tokens.Expect(TokenType.Identifier).Value;
        int startIndex = tokens.Index;

        tokens.Expect(TokenType.OpenParen);

        Arg[] funcArgs = ParseArgs();

        tokens.Expect(TokenType.CloseParen);
        tokens.Expect(TokenType.Colon);

        TypeDef returnType = ParseType();
        int endIndex = tokens.Index;

        return new()
        {
            Name = name,
            ReturnType = returnType,
            Args = funcArgs,
            TokenStart = startIndex,
            TokenEnd = endIndex
        };
    }

    private Arg[] ParseArgs()
    {
        List<Arg> args = new();
        while (tokens.Peek().Type != TokenType.CloseParen)
        {
            args.Add(ParseArg());
        }

        tokens.Expect(TokenType.CloseParen);

        return args.ToArray();
    }

    private Arg ParseArg()
    {
        string argName = tokens.Expect(TokenType.Identifier).Value;

        tokens.Expect(TokenType.Colon);

        TypeDef argType = ParseType();

        return new()
        {
            Name = argName,
            Type = argType
        };
    }

    private TypeDef ParseType()
    {
        Token typeToken = tokens.Expect(
            TokenType.Identifier,
            TokenType.IntKeyword,
            TokenType.CharKeyword,
            TokenType.BoolKeyword
        );

        ValueType type = TypeFromToken(typeToken);

        // TODO: Parse array and pointer types

        return new()
        {
            Type = type,
            IsArray = false,
            IsPointer = false
        };
    }

    private static ValueType TypeFromToken(Token tok)
    {
        switch (tok.Value)
        {
            case "int": return ValueType.Int;
            case "bool": return ValueType.Bool;
            case "char": return ValueType.Char;
            default: return ValueType.None;
        }
    }
}
