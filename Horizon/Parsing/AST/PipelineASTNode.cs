namespace Horizon.Parsing.AST;

internal class PipelineASTNode : IASTNode
{
    public IEnumerable<IASTNode> Children
    {
        get
        {
            var children = new List<IASTNode>();
            children.Add(Provider);
            children.AddRange(Processors);
            children.Add(Consumer);
            return children;
        }
    }

    public readonly IASTNode Provider;
    public readonly IASTNode Consumer;
    public readonly IASTNode[] Processors;

    public PipelineASTNode(IASTNode provider, IASTNode consumer)
        : this (provider, consumer, Array.Empty<IASTNode>()) { }

    public PipelineASTNode(IASTNode provider, IASTNode consumer, IASTNode[] processors)
    {
        Provider = provider;
        Consumer = consumer;
        Processors = processors;
    }
}
