using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    /// <summary>
    /// Class which represents the variable object. It stores the information about it.
    /// </summary>
    class Variable
    {

        private string name { get; set; } //variable name
        private int value { get; set; } //variables value (always numeric)

        /// <summary>
        /// Constructor method for the variable.
        /// </summary>
        /// <param name="name">The name to give the variable.</param>
        /// <param name="value">The value to give the variable.</param>
        public Variable(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
