using System;
using System.Collections.Generic;

namespace ase
{
    /// <summary>
    /// Class which stores the method objects and has methods to interact with them.
    /// </summary>
    public sealed class MethodStore
    {
        private static MethodStore instance = new MethodStore();
        private List<Method> methods = new List<Method>();

        static MethodStore()
        {
        }

        private MethodStore()
        {
        }

        public static MethodStore Instance { get{return instance;} }

        /// <summary>
        /// Returns the parameters which are associated with a method.
        /// </summary>
        /// <param name="index">The index in the list a method is.</param>
        /// <returns>The parameters of a method.</returns>
        public List<string> ReturnParameters(int index)
        {
            return methods[index].parameters;
        }

        
        /// <summary>
        /// Clears the method list of all entries.
        /// </summary>
        public void ClearDown(){
            methods.Clear();
        }

        /// <summary>
        /// Checks if a method has parameters or not.
        /// </summary>
        /// <param name="index">The index of a method in the list.</param>
        /// <returns>True if the method has parameters, false if the method doesn't have parameters.</returns>
        public Boolean HasParameters(int index)
        {
            if (methods[index].parameters.Count == 0){
                return false;
            }else{
                return true;
            }
        }

        /// <summary>
        /// Adds a method to the method store array.
        /// </summary>
        /// <param name="name">The name of the method to create.</param>
        /// <param name="parameters">The list of parameter names to store.</param>
        /// <param name="definition">The list of tokens that make up the method.</param>
        public void AddMethod(string name, List<string> parameters, List<Token> definition)
        {
            Method newMethod = new Method(name, parameters, definition); //new method object created
            methods.Add(newMethod); //method added
        }

        /// <summary>
        /// Returns the index of a method in the array from a given name.
        /// </summary>
        /// <param name="name">The name of the method to search for.</param>
        /// <returns>The integer index in the array of the method</returns>
        public int ReturnPosition(string name)
        {
            return methods.FindIndex(m => m.name == name); //returns index, -1 if not found
        }

        public string GetName (int index)
        {
            if (index > methods.Count && index != -1)
            {
                return "NULL";
            }
            else
            {
                return methods[index].name.ToUpper();
            }
            
        }

        /// <summary>
        /// Returns true or false if the method exists.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <returns>If the method exists, true if it does. False if it doesn't.</returns>
        public Boolean MethodExists(string name)
        {
            if (methods.FindIndex(m => m.name == name) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Returns the method body as an list of tokens which can then be executed.
        /// </summary>
        /// <param name="index">The index of the method of where it's stored in the array.</param>
        /// <returns>The tokens which the method contains.</returns>
        public List<Token> GetMethodDefinition(int index)
        {
            return methods[index].definition;
        }

        

    }
}
