using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AddMethod(string name, List<int> parameters, List<Token> definition)
        {
            Method newMethod = new Method(name, parameters, definition);
            methods.Add(newMethod);
        }

        public int ReturnPosition(string name)
        {
            return methods.FindIndex(m => m.name == name);
        }

        public List<Token> GetMethodDefinition(int index)
        {
            return methods[index].definition;
        }

        

    }
}
