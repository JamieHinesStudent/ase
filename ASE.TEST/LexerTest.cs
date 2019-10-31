using ase;
using NUnit.Framework;
using System.Collections.Generic;

namespace ASE.TEST
{
    [TestFixture]
    class LexerTest
    {
        [Test]
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

        [Test]
        public void No_Data_Return()
        {
            
            string expectedResult = "EOF"; //Expected EOF token
            var sut = new Lexer("");
            Assert.AreEqual(sut.CreateToken().tokenType.ToString(), expectedResult);
        }
    }
}
