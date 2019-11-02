using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ase.tests
{
    /// <summary>
    /// Class to test the FileManager class.
    /// </summary>
    [TestClass]
    public class FileManagerTest
    {
        /// <summary>
        /// Test which checks that the contents of a given file are correct.
        /// </summary>
        [TestMethod]
        public void Check_Loads_File()
        {
            var sut = new FileManager();
            Assert.AreEqual("circle 50", sut.LoadFile()); //Loads the file

        }
    }
}
