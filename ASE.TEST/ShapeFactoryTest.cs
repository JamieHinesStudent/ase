using ase;
using NUnit.Framework;

namespace ASE.TEST
{
    [TestFixture]
    class ShapeFactoryTest
    {
        [Test]
        public void ShapeFactory_Creates_Correct_Shape()
        {
            string[] shapes = { "Circle", "Rectangle", "Triangle" }; //Shapes to test
            string[] sutExpectedReturns = { "ase.Circle", "ase.Rectangle", "ase.Triangle" }; //Expected shapes

            for (int i = 0; i < sutExpectedReturns.Length; i++)
            {
                var sut = new ShapeFactory().getShape(shapes[i]);
                Assert.AreEqual(sutExpectedReturns[i], sut.ToString());
            }
        }
    }
}
