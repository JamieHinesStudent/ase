namespace ase
{
    /// <summary>
    /// Lexer class is responsible for taking a text input and categorising it into what it contains.
    /// </summary>
    class Lexer
    {
        private int position; //position on line character is located
        private int line; //line character is located
        private int column; //column character is located

        private char lastCharacter; //last character read in
        private readonly string script; //the whole command script

        /// <summary>
        /// Constructor class, new lexer object is created for each parse.
        /// </summary>
        /// <param name="input">The text from the command input to be parsed.</param>
        public Lexer(string input)
        {
            script = input;
            lastCharacter = script[0]; //last character is the first character initially

            //Initial positioning
            position = 0;
            line = 1;
            column = 0;
        }

        /// <summary>
        /// Gets the next character from the script input.
        /// </summary>
        /// <returns>The next character.</returns>
        private char GetNextChar()
        {
            //Updates the positioning
            position++;
            column++;

            //The EOF has been reached
            if (position >= script.Length){
                return lastCharacter = (char)0;
            }
            
            //New line has been reached
            if ((lastCharacter = script[position]) == '\n'){
                column = 1;
                line++;
            }

            return lastCharacter;
        }

        /// <summary>
        /// Creates a token which can be interpreted by the parser.
        /// </summary>
        /// <returns>A token which contains information about the input.</returns>
        public Token CreateToken()
        {
            //Character is letter 
            if (char.IsLetter(lastCharacter)) {

                string builtString = lastCharacter.ToString(); //String to be built

                //Loops around while the next characters are letters
                while (char.IsLetter(GetNextChar()))
                {
                    builtString += lastCharacter; //Builds string
                }

                //When the string is constructed it switches against it to see if it was a command
                switch (builtString.ToUpper())
                {
                    case "DRAWTO":    return new Token(Tokens.Drawto, "", line, position, column);
                    case "MOVETO":    return new Token(Tokens.Moveto, "", line, position, column);
                    case "CLEAR":     return new Token(Tokens.Clear, "", line, position, column);
                    case "RESET":     return new Token(Tokens.Reset, "", line, position, column);
                    case "RECTANGLE": return new Token(Tokens.Rectangle, "", line, position, column);
                    case "CIRCLE":    return new Token(Tokens.Circle, "", line, position, column);
                    case "TRIANGLE":  return new Token(Tokens.Triangle, "", line, position, column);
                    default:          return new Token(Tokens.Undefined, "", line, position, column); //this would be an identifier
                }
            }

            //Character is number 
            if (char.IsDigit(lastCharacter))
            {
                
                //Constructs the number
                string builtNumber = "";
                do{builtNumber += lastCharacter;} while (char.IsDigit(GetNextChar()));

                return new Token(Tokens.IntegerLiteral, builtNumber, line, position, column);
                
            }

            //EOF file has been reached
            if (lastCharacter == (char)0){
                return new Token(Tokens.EOF, "", line, position, column);
            }

            //Character is a symbol
            Tokens symbolToken = Tokens.Undefined; 
            switch (lastCharacter)
            {           
                case '\n': symbolToken = Tokens.NewLine; break;     
                case ',':  symbolToken = Tokens.Comma; break;
                case ' ':  symbolToken = Tokens.WhiteSpace; break;
                case '\t':   symbolToken = Tokens.WhiteSpace; break;
                case '\r':   symbolToken = Tokens.WhiteSpace; break;
                case (char)0: symbolToken = Tokens.EOF; break;
            }

            GetNextChar();
            return new Token(symbolToken, "", line, position, column);
        }
    }
}
