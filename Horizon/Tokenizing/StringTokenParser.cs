using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Tokenizing;

internal class StringTokenParser : TokenParser
{
    private const char QUOTE_CHR = '"';

    public StringTokenParser(TokenType type)
        : base(type) { }

    public override bool Check(string s)
    {
        for (int i = 0; i < s.Length - 1; i++)
        {
            char c = s[i];
            if (i == 0 && c != QUOTE_CHR) return false;
            if (i > 0 && c == QUOTE_CHR) return false;

            if (c == '\\') i++;
        }

        return true;
    }
}
