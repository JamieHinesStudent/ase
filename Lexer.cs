using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class Lexer
    {
        /* Holds the position of the script it's reading */
        private int position;
        private int line;
        private int column;

        /* Holds characters and the full script */
        private char lastCharacter;
        private readonly string script;

        private List<Token> tokensReturned = new List<Token>();

        public Lexer(string input)
        {
            script = input;
            lastCharacter = script[0];
            position = 0;
            line = 1;
            column = 0;
        }

        private char GetNextChar()
        {
            position++;
            column++;

            if (position >= script.Length){
                return lastCharacter = (char)0;
            }
            
            if ((lastCharacter = script[position]) == '\n'){
                column = 1;
                line++;
            }

            return lastCharacter;
        }

        public Token CreateToken()
        {
            //Character is letter 
            if (char.IsLetter(lastCharacter)) {

                string builtString = lastCharacter.ToString();

                while (char.IsLetter(GetNextChar()))
                {
                    builtString += lastCharacter;
                }

                switch (builtString.ToUpper())
                {
                    case "DRAWTO":    return new Token(Tokens.Drawto, "", line, position, column);
                    case "MOVETO":    return new Token(Tokens.Moveto, "", line, position, column);
                    case "CLEAR":     return new Token(Tokens.Clear, "", line, position, column);
                    case "RESET":     return new Token(Tokens.Reset, "", line, position, column);
                    case "RECTANGLE": return new Token(Tokens.Rectangle, "", line, position, column);
                    case "CIRCLE":    return new Token(Tokens.Circle, "", line, position, column);
                    case "TRIANGLE":  return new Token(Tokens.Triangle, "", line, position, column);
                    default:          return new Token(Tokens.Identifier, builtString.ToUpper(), line, position, column);
                }
            }

            //Character is number 
            if (char.IsDigit(lastCharacter))
            {
                
                string builtNumber = "";
                do{builtNumber += lastCharacter;} while (char.IsDigit(GetNextChar()));
                int integerNumber;
                Int32.TryParse(builtNumber, out integerNumber);
                return new Token(Tokens.IntegerLiteral, builtNumber, line, position, column);
                
            }

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
                case (char)0: symbolToken = Tokens.EOF; return new Token(Tokens.EOF, "", line, position, column);
                
                
            }
      
            GetNextChar();
            return new Token(symbolToken, "", line, position, column);
        }
    }
}
