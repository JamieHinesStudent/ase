using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ase.tests
{
    /// <summary>
    /// Class to test the Parser class.
    /// </summary>
    [TestClass]
    public class ParserTest
    {
        /// <summary>
        /// Test to check if the token type is a comma.
        /// </summary>
        [TestMethod]
        public void Check_If_Comma_True()
        {
            var sut = new Parser();
            PrivateObject obj = new PrivateObject(sut); //Allows testing of private methods
            var retVal = obj.Invoke("IsComma", new Token(Tokens.Comma, "", "", 1, 1, 1));
            Assert.AreEqual(true, retVal);
        }

        /// <summary>
        /// Test to check if the token type is a comma.
        /// </summary>
        [TestMethod]
        public void Check_If_Comma_False()
        {
            var sut = new Parser();
            PrivateObject obj = new PrivateObject(sut); //Allows testing of private methods
            var retVal = obj.Invoke("IsComma", new Token(Tokens.Clear, "", "", 1, 1, 1));
            Assert.AreEqual(false, retVal);
        }

        /// <summary>
        /// Test to check that it returns the correct number of tokens on a line.
        /// </summary>
        [TestMethod]
        public void Check_On_Line_Tokens()
        {
            var sut = new Parser();
            PrivateObject obj = new PrivateObject(sut); //Allows testing of private methods
            var retVal = obj.Invoke("tokensOnLine", new List<Token>() { new Token(Tokens.Circle, "", "", 1, 1, 1), new Token(Tokens.WhiteSpace, "", "", 1,2,2), new Token(Tokens.Clear, "", "", 2, 1,1) }, 1);
            Assert.AreEqual(2, retVal);

        }
    }
}
