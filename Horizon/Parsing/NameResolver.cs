namespace Horizon.Parsing;
internal class NameResolver
{
    private readonly Dictionary<string, List<Footprint>> functionFootprints;

    public NameResolver()
    {
        functionFootprints = new();
    }

    public void RegisterFunctions(IEnumerable<Footprint> funcFootprints)
    {
        foreach (var footprint in funcFootprints)
        {
            RegisterFunction(footprint);
        }
    }

    public void RegisterFunction(Footprint funcFootprint)
    {
        if (!functionFootprints.ContainsKey(funcFootprint.Name))
            AddFunctionName(funcFootprint.Name);

        AddFootprint(funcFootprint);
    }

    private void AddFootprint(Footprint funcFootprint)
    {
        functionFootprints[funcFootprint.Name].Add(funcFootprint);
    }

    private void AddFunctionName(string name)
    {
        functionFootprints.Add(name, new());
    }
}
