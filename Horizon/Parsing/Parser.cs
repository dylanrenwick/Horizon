﻿using Horizon.Logging;
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
        string name = tokens.Expect(TokenType.Identifier).Value;

        tokens.Expect(TokenType.OpenParen);

        Arg[] funcArgs = ParseArgs();

        tokens.Expect(TokenType.CloseParen);
        tokens.Expect(TokenType.Colon);

        TypeDef returnType = ParseType();

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
            string argName = tokens.Expect(TokenType.Identifier).Value;

            tokens.Expect(TokenType.Colon);

            TypeDef argType = ParseType();

            args.Add(new()
            {
                Name = argName,
                Type = argType
            });
        }

        return args.ToArray();
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
