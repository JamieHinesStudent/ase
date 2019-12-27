using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Windows.Forms;

namespace ase.tests
{
    /// <summary>
    /// Test class to test the commands class.
    /// </summary>
    [TestClass]
    public class CommandsTest
    {
        /// <summary>
        /// Tests the moveto command.
        /// </summary>
        [TestMethod]
        public void Test_MoveTo_Part2()
        {
            PictureBox canvas = new PictureBox();
            Bitmap drawing = new Bitmap(860, 677);
            DrawingPen canvasPen = new DrawingPen(0, 0, 860, 677);

            Commands sut = new Commands();

            sut.moveTo(canvas, drawing, canvasPen, 150, 300);
            Assert.AreEqual(150, canvasPen.xCoordinate);
            Assert.AreEqual(300, canvasPen.yCoordinate);

        }

        /// <summary>
        /// Tests the reset command.
        /// </summary>
        [TestMethod]
        public void Test_Reset_Part2()
        {
            PictureBox canvas = new PictureBox();
            Bitmap drawing = new Bitmap(860, 677);
            DrawingPen canvasPen = new DrawingPen(0, 0, 860, 677);

            Commands sut = new Commands();

            sut.moveTo(canvas, drawing, canvasPen, 150, 300);
            sut.resetPen(canvas, drawing, canvasPen);
            Assert.AreEqual(0, canvasPen.xCoordinate);
            Assert.AreEqual(0, canvasPen.yCoordinate);

        }

        /// <summary>
        /// Tests the drawto command.
        /// </summary>
        [TestMethod]
        public void Test_DrawTo_Part2()
        {
           
            PictureBox canvas = new PictureBox();
            Bitmap drawing = new Bitmap(860, 677);
            DrawingPen canvasPen = new DrawingPen(0, 0, 860, 677);

            Commands sut = new Commands();

            sut.drawTo(canvasPen, canvas, drawing, canvasPen, 10, 30);
            
            Assert.AreEqual(10, canvasPen.xCoordinate);
            Assert.AreEqual(30, canvasPen.yCoordinate);

        }
    }
}
