namespace ase
{
    /// <summary>
    /// The possible token types that the system can detect. These are used to evaluate what the input commands are.
    /// </summary>
    public enum Tokens
    {
        EOF, //End Of File
        Undefined, //Not recognised

        Clear, //Commands
        Drawto,
        Moveto,
        Reset,
        Rectangle,
        Circle,
        Triangle,
        Polygon,

        IntegerLiteral, //Types
        StringLiteral,
        Identifier,
        If,
        EndIf,
        Loop,
        EndLoop,
        While,
        Then,
        Counter,
        Method,
        EndMethod,

        Comma, //Symbols
        Plus,
        Minus,
        Multiply,
        GreaterThan,
        LessThan,
        Equals,
        OpenBracket,
        CloseBracket,
        NewLine,
        WhiteSpace,

        Colour,
    }
}
