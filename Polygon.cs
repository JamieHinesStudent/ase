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
        int[] polygonPoints;
        List<Point> points = new List<Point>();

        public Polygon():base(){}

        public override void Set(params int[] list)
        {
            base.Set(list[0], list[1]);
            polygonPoints = list.Skip(2).ToArray();
        }

        /// <summary>
        /// Method to draw the polygon.
        /// </summary>
        /// <param name="sender">Canvas to draw on.</param>
        /// <param name="drawing">Image to draw on.</param>
        public override void Draw(Object sender, Object drawing)
        {
            //Constructs the objects
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            for (int i = 0; i < polygonPoints.Length; i += 2)
            {
                points.Add(new Point(polygonPoints[i], polygonPoints[i + 1]));
            }

            g.DrawPolygon(Pens.Black, points.ToArray());

            canvas.Image = image; //Sets the bitmap image to the canvas

            g.Dispose(); //Disposes of object to free up memory
        }




    }
}
