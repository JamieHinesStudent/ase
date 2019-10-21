using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class Token
    {
        
        public Tokens tokenType { get; }
        public string value { get; }
        public int lineNumber { get; }
        public int position { get; }
        public int column { get; }


        public Token(Tokens tokenType, string value, int lineNumber, int position, int column)
        {
            this.tokenType = tokenType;
            this.value = value;
            this.lineNumber = lineNumber;
            this.position = position;
            this.column = column;

        }
    }
}
