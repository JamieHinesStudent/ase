using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class Lexer
    {
        private char lastCharacter;
        private string script;


        public Lexer(string input)
        {
            script = input;
            lastCharacter = script[0];
        }

        private char GetNextChar()
        {
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
                    case "DRAWTO": return Tokens.Drawto;
                    case "MOVETO": return Tokens.Moveto;
                    case "CLEAR": return Tokens.Clear;
                    case "RESET": return Tokens.Reset;
                    case "RECTANGLE": return Tokens.Rectangle;
                    case "CIRCLE": return Tokens.Circle;
                    case "TRIANGLE": return Tokens.Triangle;
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

            //Character is a symbol
            switch (lastCharacter)
            {
                case '\n': return Tokens.NewLine;
                case '(':  return Tokens.LeftBracket;
                case ')':  return Tokens.RightBracket;
                case ',':  return Tokens.Comma;
                case ' ':  return Tokens.WhiteSpace;
                case '+':  return Tokens.Add;
                case (char)0: return Tokens.EOF;
            }

            GetNextChar();
            return Tokens.Undefined;


        }
    }
}
