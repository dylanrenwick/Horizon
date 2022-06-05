using System.Collections;

namespace Horizon.Tokenizing;

internal class TokenStream : IEnumerator<Token>
{
    private readonly List<Token> tokens;

    private int index;

    public Token Current => tokens[index];
    object IEnumerator.Current => tokens[index];

    public int Length => tokens.Count;

    public TokenStream(List<Token> tokens)
    {
        this.tokens = tokens;
        index = 0;
    }

    public void Dispose() { }

    public bool MoveNext()
    {
        index++;
        return index < tokens.Count;
    }

    public void Reset()
    {
        index = 0;
    }

    public void Seek(int index)
    {
        this.index = index;
    }

    public Token Peek()
    {
        return tokens[index + 1];
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
        int originalIndex = index;

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
