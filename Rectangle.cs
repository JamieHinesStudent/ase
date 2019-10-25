using System;
using System.Drawing;
using System.Windows.Forms;

namespace ase
{
    class Rectangle : Shape
    { 
        int width, height;

        public Rectangle():base()
        {
            
        }

        public Rectangle(Object sender, Object drawing, Object canvasPen, int width, int height, int x, int y) : base(x, y)
        {

            this.width = width; //the only thingthat is different from shape
            this.height = height;
        }

        public override void Set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.Set(list[0], list[1]);
            this.width = list[2];
            this.height = list[3];

        }

        public override void Draw(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            g.DrawRectangle(Pens.Black, x, y, width, height);
            canvas.Image = image;

            g.Dispose();
            
        }
    }
}
