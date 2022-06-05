namespace Horizon.Parsing.AST;

internal class FunctionASTNode : IASTNode
{
    private readonly string name;
    private readonly StatementASTNode[] statements;

    public IEnumerable<IASTNode> Children => statements;
}
