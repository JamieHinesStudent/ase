using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ase.tests
{
    /// <summary>
    /// Class to test the Lexer class.
    /// </summary>
    [TestClass]
    public class LexerTest
    {
        /// <summary>
        /// Test the check that the lexer returns the correct tokens for a given input string which represents the commands.
        /// </summary>
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "Drawto", "WhiteSpace", "IntegerLiteral", "Comma", "IntegerLiteral" }; //Expected tokens

            var sut = new Lexer("Drawto 40,50"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF) //Gathers all the tokens until the end of the text
            {
                sutReturns.Add(nextToken); //Adds token to list
                nextToken = sut.CreateToken(); //Gets the token
            }

            //Assert statement
            for (int i = 0; i < sutReturns.Count; i++) { Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString()); }
        }

        /// <summary>
        /// Test the check that the lexer returns the correct token value for a given input string which is an integer.
        /// </summary>
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens_Value_For_Integer()
        {
            var sut = new Lexer("50"); //Sample command
            Token nextToken = sut.CreateToken();
            Assert.AreEqual("50", nextToken.value); //Assert statement
        }

        /// <summary>
        /// Test to check the return when an empty string is entered.
        /// </summary>
        [TestMethod]
        public void Empty_Data_Return()
        {
            var sut = new Lexer(" ");
            Assert.AreEqual(sut.CreateToken().tokenType.ToString(), "WhiteSpace");
        }

        /// <summary>
        /// Test the check that the lexer returns the correct tokens for a given input string which represents the commands for variable assigment (part 2 test).
        /// </summary>
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens_Part2()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "Identifier", "WhiteSpace", "Equals", "WhiteSpace", "IntegerLiteral" }; //Expected tokens

            var sut = new Lexer("Count = 50"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF) //Gathers all the tokens until the end of the text
            {
                sutReturns.Add(nextToken); //Adds token to list
                nextToken = sut.CreateToken(); //Gets the token
            }

            //Assert statement
            for (int i = 0; i < sutReturns.Count; i++) { Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString()); }
        }

        /// <summary>
        /// Test the check that the lexer returns the correct tokens for a given input string which represents the commands for a for loop (part 2 test).
        /// </summary>
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens_For_Loop_Part2()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "Loop", "WhiteSpace", "While", "WhiteSpace", "Identifier", "WhiteSpace", "NewLine", "WhiteSpace", "Circle", "WhiteSpace", "IntegerLiteral" }; //Expected tokens

            var sut = new Lexer("Loop while count \n circle 10"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF) //Gathers all the tokens until the end of the text
            {
                sutReturns.Add(nextToken); //Adds token to list
                nextToken = sut.CreateToken(); //Gets the token
            }

            //Assert statement
            for (int i = 0; i < sutReturns.Count; i++) { Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString()); }
        }

        /// <summary>
        /// Test to check that the lexer returns the correct tokens for a method.
        /// </summary>
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens_For_Method_Part2()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "Method", "WhiteSpace", "Identifier", "WhiteSpace", "NewLine", "WhiteSpace", "Circle", "WhiteSpace", "IntegerLiteral", "WhiteSpace", "NewLine", "WhiteSpace", "EndMethod" }; //Expected tokens

            var sut = new Lexer("Method testing \n circle 10 \n EndMethod"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF) //Gathers all the tokens until the end of the text
            {
                sutReturns.Add(nextToken); //Adds token to list
                nextToken = sut.CreateToken(); //Gets the token
            }

            //Assert statement
            for (int i = 0; i < sutReturns.Count; i++) { Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString()); }
        }

        /// <summary>
        /// Test to check the lexer returns the correct tokens for a single line if statement
        /// </summary>
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens_For_If_Part2()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "If", "WhiteSpace", "Identifier", "WhiteSpace", "LessThan", "WhiteSpace", "IntegerLiteral", "WhiteSpace", "Then", "WhiteSpace", "Circle", "WhiteSpace", "IntegerLiteral" }; //Expected tokens

            var sut = new Lexer("If X < 10 Then circle 10"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF) //Gathers all the tokens until the end of the text
            {
                sutReturns.Add(nextToken); //Adds token to list
                nextToken = sut.CreateToken(); //Gets the token
            }

            //Assert statement
            for (int i = 0; i < sutReturns.Count; i++) { Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString()); }
        }

        /// <summary>
        /// Test which checks the correct tokens are returned for a multiline if statement
        /// </summary>
        [TestMethod]
        public void Lexer_Returns_Correct_Tokens_For_MultilineIf_Part2()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "If", "WhiteSpace", "Identifier", "WhiteSpace", "LessThan", "WhiteSpace", "IntegerLiteral", "WhiteSpace", "Then", "WhiteSpace", "NewLine", 
                "WhiteSpace", "Circle", "WhiteSpace", "IntegerLiteral", "WhiteSpace", "NewLine", 
                "WhiteSpace", "Circle", "WhiteSpace", "IntegerLiteral", "WhiteSpace", "NewLine", 
                "WhiteSpace", "EndIf"}; //Expected tokens

            var sut = new Lexer("If X < 10 Then \n Circle 100 \n Circle 200 \n EndIf"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF) //Gathers all the tokens until the end of the text
            {
                sutReturns.Add(nextToken); //Adds token to list
                nextToken = sut.CreateToken(); //Gets the token
            }

            //Assert statement
            for (int i = 0; i < sutReturns.Count; i++) { Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString()); }
        }

        /// <summary>
        /// Test which makes sure the GetNextChar method returns the correct values
        /// </summary>
        [TestMethod]
        public void Get_Last_Character_Test()
        {
            var sut = new Lexer("circle 10");
            PrivateObject obj = new PrivateObject(sut); //Allows testing of private methods
            var retVal = obj.Invoke("GetNextChar");
            Assert.AreEqual('i', retVal); //Since first character is set on instance next char would be 'i'
        }

        /// <summary>
        /// Test to check the variable value is set from the lexer.
        /// </summary>
        [TestMethod]
        public void Check_Variable_Value_Part2()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = { "Identifer", "WhiteSpace", "Equals", "WhiteSpace", "IntegerLiteral" }; //Expected tokens

            var sut = new Lexer("Count = 50"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF) //Gathers all the tokens until the end of the text
            {
                sutReturns.Add(nextToken); //Adds token to list
                nextToken = sut.CreateToken(); //Gets the token
            }

            //Assert statement
            Assert.AreEqual("50", sutReturns[sutReturns.Count-1].value);

        }
    }
}
