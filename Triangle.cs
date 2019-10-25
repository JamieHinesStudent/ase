using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    /// <summary>
    /// Triangle class which extends shape. Used to draw triangles on the screen.
    /// </summary>
    class Triangle : Shape
    {
        int side1, side2, side3;

        /// <summary>
        /// Base constructor class
        /// </summary>
        public Triangle() : base()
        {
           
        }

        /// <summary>
        /// Triangle constructor class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="drawing"></param>
        /// <param name="canvasPen"></param>
        /// <param name="side1">x coordinate</param>
        /// <param name="side2">y coordinate</param>
        /// <param name="side3">x coordinate</param>
        /// <param name="x">current position of the x coordinate</param>
        /// <param name="y">current position of the y coordinate</param>
        public Triangle(int side1, int side2, int side3, int x, int y) : base(x, y)
        {

            this.side1 = side1; 
            this.side2 = side2;
            this.side3 = side3;
        }

        /// <summary>
        /// Sets the variables needed for the draw command.
        /// </summary>
        /// <param name="list">Array of integer to specifcy x and y coordinates</param>
        public override void Set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.Set(list[0], list[1]);
            this.side1 = list[2];
            this.side2 = list[3];
            this.side3 = list[4];

        }

        /// <summary>
        /// Called to draw the triangle on the screen
        /// </summary>
        /// <param name="sender">The canvas to be drawn on</param>
        /// <param name="drawing">The image to apply to the canvas</param>
        public override void Draw(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            Point[] points = { new Point(x, y), new Point(side1, side2), new Point(side3, y) };
            g.DrawPolygon(new Pen(Color.Black), points);
            canvas.Image = image;

            g.Dispose();
        }
    }
}
