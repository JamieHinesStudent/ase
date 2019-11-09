using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class Variable
    {

        private string name { get; set; }
        private int value { get; set; }

        public Variable(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
