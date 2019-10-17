using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    public enum TokenTypeEnum
    {
        EOFKeyword = 0,
        ClearKeyword = 1,
        ResetKeyword = 2,
        CircleKeyword = 3,
        RectangleKeyword = 4,
        TriangleKeyword = 5,
        MoveToKeyword = 6,
        DrawToKeyword = 7,

        StringLiteral = 100,
        IntegerLiteral = 101,

        CommaSeperator = 300,
    }
}
