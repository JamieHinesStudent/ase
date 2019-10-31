using ase;
using NUnit.Framework;

namespace ASE.TEST
{
    [TestFixture]
    class ParserTest
    {
        [Test]
        public void Check_If_Comma()
        {
            Parser target = new Parser();
            PrivateObject obj = new PrivateObject(target);
            var retVal = obj.Invoke("PrivateMethod");
            Assert.AreEqual(expectedVal, retVal);
            var sut = new Parser();
            sut.isComma;
        }
    }
}
