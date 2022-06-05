using System.Collections;

namespace Horizon.Tokenizing;

internal class TokenStream : IEnumerator<Token>
{
    private readonly List<Token> tokens;

    public int Index { get; private set; }

    public Token Current => tokens[Index];
    object IEnumerator.Current => tokens[Index];

    public int Length => tokens.Count;

    public TokenStream(List<Token> tokens)
    {
        this.tokens = tokens;
        Index = 0;
    }

    public void Dispose() { }

    public bool MoveNext()
    {
        Index++;
        return Index < tokens.Count;
    }

    public void Reset()
    {
        Index = 0;
    }

    public void Seek(int index)
    {
        this.Index = index;
    }

    public Token Peek()
    {
        return tokens[Index + 1];
    }
    public Token Pop()
    {
        MoveNext();
        return Current;
    }

    public Token Expect(params TokenType[] type)
        => Expect(t => type.Contains(t.Type));
    public Token Expect(Func<Token, bool> pred)
    {
        var token = Pop();
        if (!pred(token)) throw new Exception("Unexpected token");
        return token;
    }

    public IEnumerable<T> FromEach<T>(TokenType type, Func<T> pred)
    {
        int originalIndex = Index;

        List<T> results = new();

        Reset();
        while (MoveNext())
        {
            if (Current.Type == type)
            {
                var result = pred();
                results.Add(result);
            }                
        }

        Seek(originalIndex);

        return results;
    }
}
