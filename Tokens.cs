using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    public enum Tokens
    {
        EOF,
        Undefined,
        IntegerLiteral,
        StringLiteral,
        NewLine,
        WhiteSpace,
        Comma,
        Clear,
        Drawto,
        Moveto,
        Reset,
        Rectangle,
        Circle,
        Triangle,
        Identifier
    }
}
