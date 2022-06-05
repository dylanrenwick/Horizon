namespace Horizon.Parsing.AST;

internal class FunctionASTNode : IASTNode
{
    public readonly Footprint Footprint;

    private readonly StatementASTNode[] statements;

    public IEnumerable<IASTNode> Children => statements;
}
