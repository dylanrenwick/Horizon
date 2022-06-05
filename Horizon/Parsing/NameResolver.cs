namespace Horizon.Parsing;
internal class NameResolver
{
    private Dictionary<string, List<Footprint>> functionFootprints;

    public NameResolver()
    {
        functionFootprints = new();
    }
}
