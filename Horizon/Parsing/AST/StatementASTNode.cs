namespace Horizon.Parsing.AST;

internal abstract class StatementASTNode : IASTNode
{
    public IEnumerable<IASTNode> Children => throw new NotImplementedException();
}
