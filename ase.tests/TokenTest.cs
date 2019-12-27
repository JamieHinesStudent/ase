using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ase.tests
{
    /// <summary>
    /// Class to test the Token class.
    /// </summary>
    [TestClass]
    public class TokenTest
    {
        /// <summary>
        /// Test which checks that a token created has the correct token type.
        /// </summary>
        [TestMethod]
        public void Check_Correct_Token_Made_Type()
        {
            var sut = new Token(Tokens.Moveto, "", "", 1,1,1); //Creates a token with values
            Assert.AreEqual("Moveto", sut.tokenType.ToString()); //Assert statement
        }

        /// <summary>
        /// Test which checks that a token created has the correct line number.
        /// </summary>
        [TestMethod]
        public void Check_Correct_Token_Made_LineNumber()
        {
            var sut = new Token(Tokens.Moveto, "", "", 1, 1, 1); //Creates a token with values
            Assert.AreEqual(1, sut.lineNumber); //Assert statement
        }

        /// <summary>
        /// Test which checks that the correct tokens are created.
        /// </summary>
        [TestMethod]
        public void Check_Correct_Tokens_Part2()
        {
            var sut = new Token(Tokens.Loop, "", "", 1, 1, 1); //Creates a token with values
            Assert.AreEqual("Loop", sut.tokenType.ToString()); //Assert statement
            sut = new Token(Tokens.While, "", "", 1, 1, 1);
            Assert.AreEqual("While", sut.tokenType.ToString()); //Assert statement
            sut = new Token(Tokens.Colour, "", "", 1, 1, 1);
            Assert.AreEqual("Colour", sut.tokenType.ToString()); //Assert statement
        }
    }
}
