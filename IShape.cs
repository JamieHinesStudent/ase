using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    /// <summary>
    /// Interface shape which all of the shapes inherit their methods from. Base class of all the shapes.
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Abstract method which is used by the shapes to draw
        /// </summary>
        /// <param name="sender">The canvas which they draw on.</param>
        /// <param name="drawing">The bitmap image to draw onto</param>
       void Draw(Object sender, Object drawing);

        /// <summary>
        /// Absract method which sets the parameters of the shape so it can have sets of coordinates and sizes
        /// </summary>
        /// <param name="list">A list of parameters for the shape, differs for each shape</param>
       void Set(params int[] list);
    }
}
