using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Parsing.AST;

internal interface IASTNode
{
    public IEnumerable<IASTNode> Children { get; }
}
