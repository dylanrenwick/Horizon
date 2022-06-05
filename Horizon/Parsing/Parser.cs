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

        List<FunctionASTNode> functions = ParseFunctions();
        // TODO: Allow user-defined entry point
        FunctionASTNode? mainFunc = functions
            .Where(f => f.Footprint.Name == "main")
            .FirstOrDefault();

        if (mainFunc == null) throw new Exception("Could not find entry point 'main'");

        return new(
            mainFunc,
            functions
        );
    }

    private List<FunctionASTNode> ParseFunctions()
    {
        IEnumerable<Footprint> footprints = ParseFootprints();
        nameResolver.RegisterFunctions(footprints);

        List<FunctionASTNode> functions = new();

        foreach (var footprint in footprints)
        {
            tokens.Seek(footprint.TokenStart);

            functions.Add(ParseFunction());
        }

        return functions;
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

    private StatementASTNode ParseStatement()
    {
        throw new NotImplementedException();
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
        return tok.Value switch
        {
            "int" => ValueType.Int,
            "bool" => ValueType.Bool,
            "char" => ValueType.Char,
            _ => ValueType.None,
        };
    }
}
