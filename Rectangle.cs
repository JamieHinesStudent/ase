using System;
using System.Drawing;
using System.Windows.Forms;

namespace ase
{
    /// <summary>
    /// Class which draws a rectangle on the screen
    /// </summary>
    class Rectangle : Shape
    { 
        int width, height;

        /// <summary>
        /// Base constructor.
        /// </summary>
        public Rectangle():base()
        {
            
        }

        /// <summary>
        /// Rectangle constructor.
        /// </summary>
        /// <param name="width">width of the rectangle to be drawn.</param>
        /// <param name="height">height of the rectangle to be drawn.</param>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        public Rectangle(int width, int height, int x, int y) : base(x, y)
        {

            this.width = width; //width 
            this.height = height; //height
        }

        /// <summary>
        /// Sets the paramaters for the rectangle so it can be drawn.
        /// </summary>
        /// <param name="list">Integer list which contains the x coordinate, y coordinate, width and the height.</param>
        public override void Set(params int[] list)
        {
            base.Set(list[0], list[1]); //x and y coordinates
            width = list[2]; //width
            height = list[3]; //height

        }

        /// <summary>
        /// Draws the rectangle on the screen.
        /// </summary>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The bitmap image to draw on.</param>
        public override void Draw(Object sender, Object drawing)
        {
            //Constructs the objects
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            g.DrawRectangle(Pens.Black, x, y, width, height); //Draw function
            canvas.Image = image; //Updates canvas image

            g.Dispose();
            
        }
    }
}
