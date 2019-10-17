using System;
namespace ase
{
    public enum TokenType
    {
        EOFKeyword = 0,
        ClearKeyword = 1,
        ResetKeyword = 2,
        CircleKeyword = 3,
        RectangleKeyword = 4,
        TriangleKeyword = 5,
        MoveToKeyword = 6,

        StringLiteral = 100,
        IntegerLiteral = 101,

        CommaSeperator = 300,
    }
}