namespace Horizon.Parsing.AST;

internal class ProgramASTNode : IASTNode
{
    private readonly FunctionASTNode[] functions;
    private readonly FunctionASTNode main;

    public IEnumerable<IASTNode> Children => functions;

    public ProgramASTNode(FunctionASTNode main, IEnumerable<FunctionASTNode> funcs)
    {
        this.main = main;
        functions = funcs.ToArray();
    }
}
