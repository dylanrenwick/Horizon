namespace Horizon.Parsing.AST;

internal class FunctionASTNode : IASTNode
{
    public Footprint Footprint { get; init; }
    public StatementASTNode[] Statements { get; init; }

    public IEnumerable<IASTNode> Children => Statements;
}
