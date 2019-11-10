using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class VariableStore
    {
        private List<Token> variables;

        public VariableStore()
        {
            variables = new List<Token>();
        }

        public void ClearDown(){
            variables.Clear();
        }

        public void AddVariable(Token variable)
        {
            variables.Add(variable);
        }

        public int ReturnPosition(string variableName)
        {
            return variables.FindIndex(v => v.name == variableName);
        }

        public int ReturnValue(int index)
        {
            return Int32.Parse(variables[index].value);
        }
        public void Addition(int listPosition, int amount)
        {
            int valueAsNumber = Int32.Parse(variables[listPosition].value);
            valueAsNumber += amount;
            variables[listPosition].value = valueAsNumber.ToString().ToUpper();
        }

        public void Assign(int listPosition, int value)
        {
            variables[listPosition].value = value.ToString();
        }

        public void Multiply(int listPosition, int amount)
        {
            int valueAsNumber = Int32.Parse(variables[listPosition].value);
            valueAsNumber *= amount;
            variables[listPosition].value = valueAsNumber.ToString().ToUpper();
        }

        public void Subtract(int listPosition, int amount)
        {
            int valueAsNumber = Int32.Parse(variables[listPosition].value);
            valueAsNumber -= amount;
            variables[listPosition].value = valueAsNumber.ToString().ToUpper();
        }
    }
}
