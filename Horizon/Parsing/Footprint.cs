namespace Horizon.Parsing;

internal readonly struct Footprint
{
    public string Name { get; init; }
    public int TokenStart { get; init; }
    public int TokenEnd { get; init; }
    public TypeDef ReturnType { get; init; }
    public Arg[] Args { get; init; }
}

internal readonly struct Arg
{
    public string Name { get; init; }
    public TypeDef Type { get; init; }

    public Arg(string name, TypeDef type)
    {
        Name = name;
        Type = type;
    }
}
