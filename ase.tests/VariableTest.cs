using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ase.tests
{
    [TestClass]
    public class VariableTest
    {
        VariableStore variables = VariableStore.Instance;

        [TestMethod]
        public void Add_Variable_Test_Part2()
        {
            variables.AddVariable(new Token(Tokens.Identifier, "Test", "0", 1, 1, 1));
            variables.Addition(0, 10);
            Assert.AreEqual(variables.ReturnValue(0), 10); 

        }

        [TestMethod]
        public void Minus_Variable_Test_Part2()
        {
            variables.Subtract(0, 1);
            Assert.AreEqual(variables.ReturnValue(0), 9); 
        }

        [TestMethod]
        public void Multiply_Variable_Test_Part2()
        {
            variables.Multiply(0, 10);
            Assert.AreEqual(variables.ReturnValue(0), 90);
        }

        [TestMethod]
        public void Assign_Variable_Test_Part2()
        {
            variables.Assign(0, 10);
            Assert.AreEqual(variables.ReturnValue(0), 10);
        }

        [TestMethod]
        public void Clear_Variable_List_Part2()
        {
            variables.ClearDown();
            Assert.AreEqual(variables.ReturnPosition("Test"), -1);

        }
    }
}
