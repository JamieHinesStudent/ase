using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    class Commands
    {

        /** Clear screen **/
        public void clearScreen(Object sender)
        {
            PictureBox canvas = (PictureBox)sender;
            canvas.Image = null;
        }

        /** Reset pen **/
        public void resetPen()
        {

        }

        /** Set pen **/
        public void setPen(System.Drawing.Pen pen, Object sender, Object drawing, int x, int y)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;

            


        }

        /** Draw to **/


    }
}
