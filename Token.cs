﻿namespace ase
{
    ///<summary>
    ///Class which stores information about the tokens returned by the lexer class.
    ///</summary>
    public class Token
    {
        
        public Tokens tokenType { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public int lineNumber { get; set; }
        public int position { get; }
        public int column { get; }

        /// <summary>
        /// Constructor which builds a token taking in parameters which define the token.
        /// </summary>
        /// <param name="tokenType">The type of the token returned by the lexer.</param>
        /// <param name="value">The tokens value used for storing string and integer values.</param>
        /// <param name="lineNumber">The line number where the token is.</param>
        /// <param name="position">The position on the line where the token is.</param>
        /// <param name="column">The column where the token is located.</param>
        public Token(Tokens tokenType, string name, string value, int lineNumber, int position, int column)
        {
            this.tokenType = tokenType;
            this.name = name;
            this.value = value;
            this.lineNumber = lineNumber;
            this.position = position;
            this.column = column;
        }
    }
}
