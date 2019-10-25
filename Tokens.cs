using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    /// <summary>
    /// The possible token types that the system can detect. These are used to evaluate what the input commands are.
    /// </summary>
    public enum Tokens
    {
        EOF, //End Of File
        Undefined, //Not recognised
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
