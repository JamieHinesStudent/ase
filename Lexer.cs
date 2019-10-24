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

        private List<Token> tokensReturned = new List<Token>();

        
        public Token peekList(int index)
        {
            return tokensReturned[index];
        }



        public void removeSpacesList()
        {
            try
            {
                var itemToRemove = tokensReturned.Single(t => t.tokenType.ToString() == "WhiteSpace");
                tokensReturned.Remove(itemToRemove);
            }
            catch (Exception e)
            {

            }
        }

        public int listLength()
        {
            return tokensReturned.Count;
        }

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
                    case "DRAWTO":    tokensReturned.Add(new Token(Tokens.Drawto, "", line, position, column)); return Tokens.Drawto;
                    case "MOVETO":    tokensReturned.Add(new Token(Tokens.Moveto, "", line, position, column)); return Tokens.Moveto;
                    case "CLEAR":     tokensReturned.Add(new Token(Tokens.Clear, "", line, position, column)); return Tokens.Clear;
                    case "RESET":     tokensReturned.Add(new Token(Tokens.Reset, "", line, position, column)); return Tokens.Reset;
                    case "RECTANGLE": tokensReturned.Add(new Token(Tokens.Rectangle, "", line, position, column)); return Tokens.Rectangle;
                    case "CIRCLE":    tokensReturned.Add(new Token(Tokens.Circle, "", line, position, column)); return Tokens.Circle;
                    case "TRIANGLE":  tokensReturned.Add(new Token(Tokens.Triangle, "", line, position, column)); return Tokens.Triangle;
                    default:          tokensReturned.Add(new Token(Tokens.Identifier, builtString.ToUpper(), line, position, column)); return Tokens.Identifier;
                }
            }

            //Character is number 
            if (char.IsDigit(lastCharacter))
            {
                string builtNumber = "";
                do{builtNumber += lastCharacter;} while (char.IsDigit(GetNextChar()));
                int integerNumber;
                Int32.TryParse(builtNumber, out integerNumber);
                tokensReturned.Add(new Token(Tokens.IntegerLiteral, builtNumber, line, position, column));
                return Tokens.IntegerLiteral;
            }

            if (lastCharacter == (char)0){
                tokensReturned.Add(new Token(Tokens.EOF, "", line, position, column));
            }

            //Character is a symbol
            Tokens symbolToken = Tokens.Undefined; 
            switch (lastCharacter)
            {
                case '\n': symbolToken = Tokens.NewLine; break;
                /*case '(':  symbolToken = Tokens.LeftBracket; break;*/
                /*case ')':  symbolToken = Tokens.RightBracket; break;*/
                case ',':  symbolToken = Tokens.Comma; break;
                case ' ':  symbolToken = Tokens.WhiteSpace; break;
                /*case '+':  symbolToken = Tokens.Add; break;*/
                /*case '=': symbolToken  = Tokens.Equals; break;*/
                case (char)0: symbolToken = Tokens.EOF; return Tokens.EOF;
                
            }

            GetNextChar();
            tokensReturned.Add(new Token(symbolToken, "", line, position, column));
            return symbolToken;
        }
    }
}
