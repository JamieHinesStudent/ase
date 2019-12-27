namespace ase
{
    /// <summary>
    /// The possible token types that the system can detect. These are used to evaluate what the input commands are.
    /// </summary>
    public enum Tokens
    {
        /// <summary>
        /// End of file.
        /// </summary>
        EOF, //End Of File

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined, //Not recognised

        /// <summary>
        /// Clear command.
        /// </summary>
        Clear, //Commands

        /// <summary>
        /// Drawto command.
        /// </summary>
        Drawto,

        /// <summary>
        /// Moveto command.
        /// </summary>
        Moveto,

        /// <summary>
        /// Reset command.
        /// </summary>
        Reset,

        /// <summary>
        /// Rectangle command.
        /// </summary>
        Rectangle,

        /// <summary>
        /// Circle command.
        /// </summary>
        Circle,

        /// <summary>
        /// Triangle command.
        /// </summary>
        Triangle,

        /// <summary>
        /// Polygon command.
        /// </summary>
        Polygon,

        /// <summary>
        /// Integer.
        /// </summary>
        IntegerLiteral, //Types

        /// <summary>
        /// String.
        /// </summary>
        StringLiteral,

        /// <summary>
        /// Identifier (either a method or variable).
        /// </summary>
        Identifier,

        /// <summary>
        /// If keyword.
        /// </summary>
        If,

        /// <summary>
        /// EndIf keyword
        /// </summary>
        EndIf,

        /// <summary>
        /// Loop keyword.
        /// </summary>
        Loop,

        /// <summary>
        /// EndLoop keyword.
        /// </summary>
        EndLoop,

        /// <summary>
        /// While keyword.
        /// </summary>
        While,

        /// <summary>
        /// Then keyword.
        /// </summary>
        Then,

        /// <summary>
        /// Counter keyword.
        /// </summary>
        Counter,

        /// <summary>
        /// Method keyword.
        /// </summary>
        Method,

        /// <summary>
        /// EndMethod keyword.
        /// </summary>
        EndMethod,

        /// <summary>
        /// Comma keyword.
        /// </summary>
        Comma, //Symbols

        /// <summary>
        /// Plus action.
        /// </summary>
        Plus,

        /// <summary>
        /// Minus action.
        /// </summary>
        Minus,

        /// <summary>
        /// Mulitply action.
        /// </summary>
        Multiply,

        /// <summary>
        /// GreaterThan action.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Less than action.
        /// </summary>
        LessThan,

        /// <summary>
        /// Equals action.
        /// </summary>
        Equals,

        /// <summary>
        /// OpenBracket action.
        /// </summary>
        OpenBracket,

        /// <summary>
        /// CloseBracket action.
        /// </summary>
        CloseBracket,

        /// <summary>
        /// NewLine character.
        /// </summary>
        NewLine,

        /// <summary>
        /// WhiteSpace character.
        /// </summary>
        WhiteSpace,

        /// <summary>
        /// Colour command.
        /// </summary>
        Colour,
    }
}
