namespace Horizon.Parsing.AST;

internal class ProgramASTNode : IASTNode
{
    private readonly FunctionASTNode[] functions;
    private readonly FunctionASTNode main;

    public IEnumerable<IASTNode> Children => functions;
}
