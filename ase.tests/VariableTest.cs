using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ase.tests
{
    /// <summary>
    /// Class to test the variables in the program.
    /// </summary>
    [TestClass]
    public class VariableTest
    {
        VariableStore variables = VariableStore.Instance; //new instance

        /// <summary>
        /// Test to check variable addition.
        /// </summary>
        [TestMethod]
        public void Add_Variable_Test_Part2()
        {
            variables.AddVariable(new Token(Tokens.Identifier, "Test", "0", 1, 1, 1));
            variables.Addition(0, 10);
            Assert.AreEqual(variables.ReturnValue(0), 10); 

        }

        /// <summary>
        /// Test to check variable subtraction.
        /// </summary>
        [TestMethod]
        public void Minus_Variable_Test_Part2()
        {
            variables.AddVariable(new Token(Tokens.Identifier, "Tester", "10", 1, 1, 1));
            variables.Subtract(variables.ReturnPosition("Tester"), 1);
            Assert.AreEqual(variables.ReturnValue(0), 9); 
        }

        /// <summary>
        /// Test to check variable multiplication.
        /// </summary>
        [TestMethod]
        public void Multiply_Variable_Test_Part2()
        {
            variables.Multiply(0, 10);
            Assert.AreEqual(variables.ReturnValue(0), 90);
        }

        /// <summary>
        /// Test to check variable assignment.
        /// </summary>
        [TestMethod]
        public void Assign_Variable_Test_Part2()
        {
            variables.Assign(0, 10);
            Assert.AreEqual(variables.ReturnValue(0), 10);
        }

        /// <summary>
        /// Test to check variable store clear down.
        /// </summary>
        [TestMethod]
        public void Clear_Variable_List_Part2()
        {
            variables.ClearDown();
            Assert.AreEqual(variables.ReturnPosition("Test"), -1);

        }
    }
}
