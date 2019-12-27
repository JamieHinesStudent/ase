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
    /// Class which draws the polygon shape.
    /// </summary>
    class Polygon : Shape
    {
        int[] polygonPoints; //points array
        List<Point> points = new List<Point>(); //points list

        /// <summary>
        /// Base constructor.
        /// </summary>
        public Polygon():base(){}

        /// <summary>
        /// Sets the parameters for the shape.
        /// </summary>
        /// <param name="list">The x,y coordinates and the points to draw.</param>
        public override void Set(params int[] list)
        {
            base.Set(list[0], list[1]);
            polygonPoints = list.Skip(2).ToArray();
        }

        /// <summary>
        /// Method to draw the polygon.
        /// </summary>
        /// <param name="Pen">The pen object to draw with.</param>
        /// <param name="sender">Canvas to draw on.</param>
        /// <param name="drawing">Image to draw on.</param>
        public override void Draw(Object Pen, Object sender, Object drawing)
        {
            //Constructs the objects
            DrawingPen colouredPen = (DrawingPen)Pen;
            Pen penColour = colouredPen.returnColour();
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            //Loops over the points and creates x,y coordinates
            for (int i = 0; i < polygonPoints.Length; i += 2)
            {
                points.Add(new Point(polygonPoints[i], polygonPoints[i + 1]));
            }

            g.DrawPolygon(penColour, points.ToArray()); //draws the shape

            canvas.Image = image; //Sets the bitmap image to the canvas

            g.Dispose(); //Disposes of object to free up memory
        }




    }
}
