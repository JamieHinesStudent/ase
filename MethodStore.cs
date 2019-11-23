using System;
using System.Collections.Generic;

namespace ase
{
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

        public List<string> ReturnParameters(int index)
        {
            return methods[index].parameters;
        }

        

        public void ClearDown(){
            methods.Clear();
        }

        public Boolean HasParameters(int index)
        {
            if (methods[index].parameters.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public void AddMethod(string name, List<string> parameters, List<Token> definition)
        {
            Method newMethod = new Method(name, parameters, definition);
            methods.Add(newMethod);
        }

        public int ReturnPosition(string name)
        {
            return methods.FindIndex(m => m.name == name);
        }

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
