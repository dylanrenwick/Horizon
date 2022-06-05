using Horizon.Logging;
using Horizon.Parsing.AST;
using Horizon.Tokenizing;

namespace Horizon.Parsing;

internal class Parser
{
    private Logger log;

    private TokenStream tokens;

    public Parser(Logger logger)
    {
        log = logger;
    }

    public ProgramASTNode Parse(TokenStream tokens)
    {
        this.tokens = tokens;
    }

    private IEnumerable<FunctionASTNode> ParseFunctions()
    {

    }

    private IEnumerable<Footprint> ParseFootprint()
    {

    }

    private Footprint ParseFunctionFootprint()
    {
        var name = tokens.Expect(TokenType.Identifier).Value;

        tokens.Expect(TokenType.OpenParen);

        var funcArgs = ParseArgs();

        tokens.Expect(TokenType.CloseParen);
        tokens.Expect(TokenType.Colon);

        var returnType = ParseType();

        return new()
        {
            Name = name,
            ReturnType = returnType,
            Args = funcArgs
        };
    }

    private Arg[] ParseArgs()
    {
        List<Arg> args = new();
        while (tokens.Peek().Type != TokenType.CloseParen)
        {
            var argName = tokens.Expect(TokenType.Identifier);
            tokens.Expect(TokenType.Colon);

            var argType = ParseType();

            args.Add(new()
            {
                Name = argName.Value,
                Type = argType
            });
        }

        return args.ToArray();
    }

    private TypeDef ParseType()
    {
        var typeName = tokens.Expect(TokenType.Identifier, TokenType.Keyword);

        ValueType type = typeName.Value switch
        {
            "int" => ValueType.Int,
            "bool" => ValueType.Bool,
            "char" => ValueType.Char,
            _ => ValueType.None
        };

        // TODO: Parse array and pointer types

        return new()
        {
            Type = type,
            IsArray = false,
            IsPointer = false
        };
    }
}
