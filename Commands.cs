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
    /// Class which provides the commands to draw on the canvas.
    /// </summary>
    class Commands
    {
        /// <summary>
        /// Clears the screen of any drawings made.
        /// </summary>
        /// <param name="sender">The canvas.</param>
        /// <param name="drawing">The bitmap image.</param>
        public void clearScreen(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Transparent);
            canvas.Image = image;
            g.Dispose();
        }

        /// <summary>
        /// Resets the coordinates of the pen to x:0 and y:0.
        /// </summary>
        /// <param name="sender">The canvas.</param>
        /// <param name="drawing">The bitmap image.</param>
        /// <param name="canvasPen">The pen object where the x and y coordinates are stored.</param>
        public void resetPen(Object sender, Object drawing, Object canvasPen)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.TranslateTransform(0, 0);

            local.xCoordinate = 0; //Reset x coordinate to 0
            local.yCoordinate = 0; //Reset y coordinate to 0
            canvas.Image = image; //Update image
        }

        /// <summary>
        /// Draws a line from the current x,y coordinates to the given x,y coordinates.
        /// </summary>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to darw on.</param>
        /// <param name="canvasPen">The pen object which stores x,y coordinates.</param>
        /// <param name="x">The x coordinate to draw to.</param>
        /// <param name="y">The y coordinate to draw to.</param>
        public void drawTo(Object Pen, Object sender, Object drawing, Object canvasPen, int x, int y)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.DrawLine(new Pen(Color.Black), local.xCoordinate, local.yCoordinate, x, y); //Draw line command
            local.xCoordinate = x;
            local.yCoordinate = y;
            canvas.Image = image; //Updates the image

        }

        /// <summary>
        /// Moves the pen to a given x and y coordinate.
        /// </summary>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        /// <param name="canvasPen">The pen object.</param>
        /// <param name="x">X coordinate to move to.</param>
        /// <param name="y">Y coordinate to move to.</param>
        public void moveTo(Object sender, Object drawing, Object canvasPen, int x, int y)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.TranslateTransform(x, y); //Sets x and y coordinates
            local.xCoordinate = x; //Updates the x coordinate in the pen object
            local.yCoordinate = y; //Updates the y coordinate in the pen object
            canvas.Image = image; //Updates the image
        }
    }
}
