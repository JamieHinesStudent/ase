using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    public enum Tokens
    {

        EOF = 0,
        Undefined = 1,
        IF = 2,
        ELSEIF = 3,
        ELSE = 4,
        ENDIF = 5,
        LOOP = 6,
        ENDLOOP = 7,
        FOR = 8,
        METHOD = 9,
        ENDMETHOD = 10,
        IntegerLiteral = 11,
        StringLiteral = 12,


        Add = 13,
        Equals = 14,
        NewLine = 15,
        WhiteSpace = 16,
        Comma = 17,
        LeftBracket = 18,
        RightBracket = 19,

        //Commands
        Clear = 20,
        Drawto = 21,
        Moveto = 22,
        Reset = 23,

        Rectangle = 24,
        Circle = 25,
        Triangle = 26,
        Identifier
    }
}
