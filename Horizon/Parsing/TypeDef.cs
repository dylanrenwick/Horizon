namespace Horizon.Parsing;

internal readonly struct TypeDef
{
    public ValueType Type { get; init; }
    public bool IsArray { get; init; }
    public bool IsPointer { get; init; }
}

internal enum ValueType
{
    None,
    Int,
    Bool,
    Char
}
