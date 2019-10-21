using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class Lexer
    {
        /* Holds the position of the script it's reading **/
        private int position;
        private int line;
        private int column;

        /* Holds characters and the full script */
        private char lastCharacter;
        private string script;


        public Lexer(string input)
        {
            script = input;
            lastCharacter = script[0];
            position = 0;
            line = 0;
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

        public Tokens CreateToken()
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
                    case "DRAWTO":    return Tokens.Drawto;
                    case "MOVETO":    return Tokens.Moveto;
                    case "CLEAR":     return Tokens.Clear;
                    case "RESET":     return Tokens.Reset;
                    case "RECTANGLE": return Tokens.Rectangle;
                    case "CIRCLE":    return Tokens.Circle;
                    case "TRIANGLE":  return Tokens.Triangle;
                    default: return Tokens.Identifier;
                }
            }

            //Character is number 
            if (char.IsDigit(lastCharacter))
            {
                string builtNumber = "";
                do{builtNumber += lastCharacter;} while (char.IsDigit(GetNextChar()));
                int integerNumber;
                Int32.TryParse(builtNumber, out integerNumber);
                return Tokens.IntegerLiteral;
            }

            if (lastCharacter == (char)0){
                System.Diagnostics.Debug.WriteLine("EOF");
            }

            //Character is a symbol
            Tokens symbolToken = Tokens.Undefined; 
            switch (lastCharacter)
            {
                case '\n': symbolToken = Tokens.NewLine; break;
                case '(':  symbolToken = Tokens.LeftBracket; break;
                case ')':  symbolToken = Tokens.RightBracket; break;
                case ',':  symbolToken = Tokens.Comma; break;
                case ' ':  symbolToken = Tokens.WhiteSpace; break;
                case '+':  symbolToken = Tokens.Add; break;
                case '=': symbolToken  = Tokens.Equals; break;
                case (char)0: return Tokens.EOF;
                
            }

            GetNextChar();
            return symbolToken;
        }
    }
}
