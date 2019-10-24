using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    class Circle : Shape
    {
        int radius;

        public Circle():base()
        {

        }

        public Circle(Object sender, Object drawing, Object canvasPen, int radius, int x, int y) : base(x, y)
        {

            this.radius = radius; //the only thingthat is different from shape
        }

        public override void Set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is radius
            base.Set(list[0], list[1]);
            this.radius = list[2];
            
        }

        public override void Draw(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            Pen mypen = new Pen(Color.Black);

            g.DrawEllipse(Pens.Red, x, y, radius * 2, radius * 2);
            canvas.Image = image;

            g.Dispose();
        }
    }
}