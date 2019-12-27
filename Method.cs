using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    /// <summary>
    /// Class which stores information about methods.
    /// </summary>
    public class Method
    {
        /// <summary>
        /// Name of the method.
        /// </summary>
        public string name { get; set; } //name of the method
        /// <summary>
        /// Parameters for the method.
        /// </summary>
        public List<string> parameters { get; set; } //parameters the method can except
        /// <summary>
        /// Method definition, the tokens that make it up.
        /// </summary>
        public List<Token> definition { get;} //the tokens which make up the method

        /// <summary>
        /// Method constructor.
        /// </summary>
        /// <param name="name">The name of the method to create.</param>
        /// <param name="parameters">The parameters the method will accept, this can be an empty list.</param>
        /// <param name="definition">The tokens which make up the method.</param>
        public Method(string name, List<string> parameters, List<Token> definition)
        {
            this.name = name;
            this.parameters = parameters;
            this.definition = definition;
        }
    }
}
