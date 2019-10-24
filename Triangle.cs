using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    class Triangle : Shape
    {
        int side1, side2, side3;

        public Triangle() : base()
        {
           
        }

        public Triangle(Object sender, Object drawing, Object canvasPen, int side1, int side2, int side3, int x, int y) : base(x, y)
        {

            this.side1 = side1; //the only thingthat is different from shape
            this.side2 = side2;
            this.side3 = side3;
        }

        public override void Set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.Set(list[0], list[1]);
            this.side1 = list[2];
            this.side2 = list[3];
            this.side3 = list[4];

        }

        public override void Draw(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            Pen mypen = new Pen(Color.Black);

            Point[] points = { new Point(10, 10), new Point(100, 10), new Point(50, 100) };
            g.DrawPolygon(new Pen(Color.Blue), points);
            canvas.Image = image;

            g.Dispose();
        }
    }
}
