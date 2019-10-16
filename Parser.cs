using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    class Parser
    {

        private string[] splitLines(string command)
        {
            return command.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
        }

        public void callParser(string text)
        {
            string[] programLines = splitLines(text);
            Console.WriteLine(programLines.Length);
        }



        public void testDraw(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g;
            g = Graphics.FromImage(image);

            Pen mypen = new Pen(Color.Black);

            g.DrawLine(mypen, 0, 0, 500, 150);

            g.DrawEllipse(Pens.Red, 50, 50, 20, 20);

            g.DrawRectangle(Pens.Blue, 50,50,100,100);

            Point[] points = { new Point(10, 10), new Point(100, 10), new Point(50, 100) };
            g.DrawPolygon(new Pen(Color.Blue), points);
            

            canvas.Image = image;

            g.Dispose();
        }
        
        
    }
}
