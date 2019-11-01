using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ase.tests
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "Drawto", "WhiteSpace", "IntegerLiteral", "Comma", "IntegerLiteral" }; //Expected tokens

            var sut = new Lexer("Drawto 40,50"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF)
            {
                sutReturns.Add(nextToken);
                nextToken = sut.CreateToken();
            }

            //Assert statement
            for (int i = 0; i < sutReturns.Count; i++) { Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString()); }
        }

        [TestMethod]
        public void Empty_Data_Return()
        {
            var sut = new Lexer(" ");
            Assert.AreEqual(sut.CreateToken().tokenType.ToString(), "WhiteSpace");
        }
    }
}
