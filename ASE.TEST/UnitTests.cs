using System.Collections.Generic;
using ase;
using NUnit.Framework;

namespace ASE.TEST
{
    /// <summary>
    /// Class which contains the unit test to test the ase namespace classes to make sure they peform as expected.
    /// </summary>
    [TestFixture]
    class UnitTests
    {

        //Tests for part 1

        /// <summary>
        /// Unit test which makes sure that the types of the tokens returned for a command is correct.
        /// </summary>
        [Test]
        public void Lexer_Returns_Correct_Tokens()
        {
            List<Token> sutReturns = new List<Token>(); //List to store the returned tokens
            string[] sutExpectedReturns = {"Drawto","WhiteSpace","IntegerLiteral","Comma","IntegerLiteral"}; //Expected tokens
            
            var sut = new Lexer("Drawto 40,50"); //Sample command
            Token nextToken = sut.CreateToken();
            while (nextToken.tokenType != Tokens.EOF)
            {
                sutReturns.Add(nextToken);
                nextToken = sut.CreateToken();
            }

            //Assert statement
            for (int i=0; i<sutReturns.Count; i++){Assert.AreEqual(sutExpectedReturns[i], sutReturns[i].tokenType.ToString());}  
        }

        [Test]
        public void ShapeFactory_Creates_Correct_Shape()
        {
            string[] shapes = {"Circle","Rectangle","Triangle" }; //Shapes to test
            string[] sutExpectedReturns = { "ase.Circle", "ase.Rectangle", "ase.Triangle"}; //Expected shapes

            for (int i=0; i<sutExpectedReturns.Length; i++)
            {
                var sut = new ShapeFactory().getShape(shapes[i]);
                Assert.AreEqual(sutExpectedReturns[i], sut.ToString());
            }
        }

        [Test]
        public void Out_Of_Bounds_Test()
        {
            var sut = new DrawingPen(0, 0, 100, 100);
            Assert.AreEqual(sut.CheckDimensions(101,101), false);
            Assert.AreEqual(sut.CheckDimensions(99, 101), false);
            Assert.AreEqual(sut.CheckDimensions(101, 99), false);
            Assert.AreEqual(sut.CheckDimensions(-1, 101), false);
        }

    }
}
