using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    /// <summary>
    /// Singleton class which allows you to perform actions on the variables. Stores all the variables in the program.
    /// </summary>
    public sealed class VariableStore
    {
        private static readonly VariableStore instance = new VariableStore();
        private List<Token> variables = new List<Token>(); //list to hold all the created variables

        /// <summary>
        /// VariableStore constructor.
        /// </summary>
        static VariableStore()
        {
        }

        /// <summary>
        /// VariableStore constructor.
        /// </summary>
        private VariableStore()
        {
        }

        /// <summary>
        /// Returns the instance of the VariableStore.
        /// </summary>
        public static VariableStore Instance{
            get{
                return instance;
            }
        }

        /// <summary>
        /// Removes all the variables which the program has stored.
        /// </summary>
        public void ClearDown(){
            variables.Clear();
        }

        /// <summary>
        /// Adds a variable to the stored list.
        /// </summary>
        /// <param name="variable">The variable to add.</param>
        public void AddVariable(Token variable)
        {
            variables.Add(variable);
        }

        /// <summary>
        /// Returns the position in the list where the variable is.
        /// </summary>
        /// <param name="variableName">The variable name to look for.</param>
        /// <returns>The index in the list of the variable (-1) if not found.</returns>
        public int ReturnPosition(string variableName)
        {
            return variables.FindIndex(v => v.name == variableName);
        }

        /// <summary>
        /// Returns the value of a given variable in the list based on the index.
        /// </summary>
        /// <param name="index">The index in the list of the variable whos value you want.</param>
        /// <returns>The variable value.</returns>
        public int ReturnValue(int index)
        {
            return Int32.Parse(variables[index].value);
        }

        /// <summary>
        /// Add to a given variable.
        /// </summary>
        /// <param name="listPosition">The position in the list of the variable you want to add to.</param>
        /// <param name="amount">The amount you want to add.</param>
        public void Addition(int listPosition, int amount)
        {
            int valueAsNumber = Int32.Parse(variables[listPosition].value);
            valueAsNumber += amount;
            variables[listPosition].value = valueAsNumber.ToString().ToUpper();
        }

        /// <summary>
        /// Assign to a given variable.
        /// </summary>
        /// <param name="listPosition">The position in the list of the variable you want to assign to.</param>
        /// <param name="value">The new value for the variable.</param>
        public void Assign(int listPosition, int value)
        {
            variables[listPosition].value = value.ToString();
        }

        /// <summary>
        /// Multiply a given variable.
        /// </summary>
        /// <param name="listPosition">The position in the list of the variable you want to assign to.</param>
        /// <param name="amount">The value to multiply by.</param>
        public void Multiply(int listPosition, int amount)
        {
            int valueAsNumber = Int32.Parse(variables[listPosition].value);
            valueAsNumber *= amount;
            variables[listPosition].value = valueAsNumber.ToString().ToUpper();
        }

        /// <summary>
        /// Subtract from a given variable.
        /// </summary>
        /// <param name="listPosition">The position in the list of the variable you want to assign to.</param>
        /// <param name="amount">The amount to subtract from the variable.</param>
        public void Subtract(int listPosition, int amount)
        {
            int valueAsNumber = Int32.Parse(variables[listPosition].value);
            valueAsNumber -= amount; //subtracts amount
            variables[listPosition].value = valueAsNumber.ToString().ToUpper(); //reassigns value
        }
    }
}
