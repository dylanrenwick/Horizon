namespace Horizon.Tokenizing;

internal enum TokenType
{
    // Control
    None,
    EOF,
    Newline,
    Whitespace,
    // Literals
    Integer,
    Float,
    String,
    // Operators
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulus,
    MemberAccess,
}
