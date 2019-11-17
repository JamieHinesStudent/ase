using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    public class Method
    {
        public string name { get; set; }
        public List<int> parameters { get; set; }
        public List<Token> definition { get; set; }

        public Method(string name, List<int> parameters, List<Token> definition)
        {
            this.name = name;
            this.parameters = parameters;
            this.definition = definition;
        }
    }
}
