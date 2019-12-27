using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ase.tests
{
    /// <summary>
    /// Test class to test the method classes.
    /// </summary>
    [TestClass]
    public class MethodTest
    {
        private MethodStore methods = MethodStore.Instance; //method instance

        /// <summary>
        /// Test to check method with no parameters can be created.
        /// </summary>
        [TestMethod]
        public void Set_Method_No_Parameters_Part2()
        {
            methods.AddMethod("Test", new List<string> { }, new List<Token> { new Token(Tokens.Circle, "", "", 1, 1, 1), new Token(Tokens.IntegerLiteral, "", "10",1,2,1)});
            Assert.AreNotEqual(methods.ReturnPosition("Test"), -1);
        }

        /// <summary>
        /// Test to return the method definition of a created method.
        /// </summary>
        [TestMethod]
        public void Get_Method_Definition_Part2()
        {
            methods.AddMethod("TestTwo", new List<string> { }, new List<Token> { new Token(Tokens.Circle, "", "", 1, 1, 1)});
            Assert.AreEqual(methods.GetMethodDefinition(methods.ReturnPosition("TestTwo"))[0].tokenType, Tokens.Circle);
        }

        /// <summary>
        /// Test to check a method can be created with no parameters.
        /// </summary>
        [TestMethod]
        public void Set_Method_With_Parameters_Part2()
        {
            methods.AddMethod("TestOne", new List<string> {"One","Two"}, new List<Token> { new Token(Tokens.Circle, "", "", 1, 1, 1), new Token(Tokens.IntegerLiteral, "", "10", 1, 2, 1) });
            Assert.AreEqual(methods.HasParameters(methods.ReturnPosition("TestOne")), true);
        }

        /// <summary>
        /// Test to check the parameter names can be returned for a given method.
        /// </summary>
        [TestMethod]
        public void Get_Method_Parameter_Names_Part2()
        {
            methods.AddMethod("TestThree", new List<string> { "One", "Two" }, new List<Token> { new Token(Tokens.Circle, "", "", 1, 1, 1), new Token(Tokens.IntegerLiteral, "", "10", 1, 2, 1) });
            List<string> sut = methods.ReturnParameters(methods.ReturnPosition("TestThree"));
            Assert.AreEqual(sut[0], "One");
            Assert.AreEqual(sut[1], "Two");


        }
    }
}
