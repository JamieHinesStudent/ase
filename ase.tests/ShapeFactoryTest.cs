using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ase.tests
{
    /// <summary>
    /// Class to test the ShapeFactory class.
    /// </summary>
    [TestClass]
    public class ShapeFactoryTest
    {
        /// <summary>
        /// Test which checks to make sure the shape factory creates the correct shape for a given input.
        /// </summary>
        [TestMethod]
        public void ShapeFactory_Creates_Correct_Shape()
        {
            string[] shapes = { "Circle", "Rectangle", "Triangle" }; //Shapes to test
            string[] sutExpectedReturns = { "ase.Circle", "ase.Rectangle", "ase.Triangle" }; //Expected shapes

            for (int i = 0; i < sutExpectedReturns.Length; i++)
            {
                var sut = new ShapeFactory().getShape(shapes[i]); //Creates the shape
                Assert.AreEqual(sutExpectedReturns[i], sut.ToString()); //Assert statement
            }
        }

        /// <summary>
        /// Test which checks to make sure that the shape factory creates the correct shape for a given input (part 2 test).
        /// </summary>
        [TestMethod]
        public void ShapeFactory_Creates_Correct_Shape_Part2()
        {
            string[] shapes = { "Circle", "Rectangle", "Triangle", "Polygon" }; //Shapes to test
            string[] sutExpectedReturns = { "ase.Circle", "ase.Rectangle", "ase.Triangle", "ase.Polygon" }; //Expected shapes

            for (int i = 0; i < sutExpectedReturns.Length; i++)
            {
                var sut = new ShapeFactory().getShape(shapes[i]); //Creates the shape
                Assert.AreEqual(sutExpectedReturns[i], sut.ToString()); //Assert statement
            }
        }
    }
}
