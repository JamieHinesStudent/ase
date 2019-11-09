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

        IntegerLiteral, //Types
        StringLiteral,
        Identifier,
        If,
        EndIf,
        Else,
        Loop,
        EndLoop,
        For,
        Then,
        Counter,
        Method,

        Comma, //Symbols
        Plus,
        Minus,
        Multiply,
        Divide,
        GreaterThan,
        LessThan,
        Equals,
        OpenBracket,
        CloseBracket,
        NewLine,
        WhiteSpace,
    }
}
