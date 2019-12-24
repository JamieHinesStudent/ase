﻿using System;

namespace ase
{
    /// <summary>
    /// Interface shape which all of the shapes inherit their methods from.
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Method which is used by the shapes to draw.
        /// </summary>
        /// <param name="Pen">The pen object to draw with.</param>
        /// <param name="sender">The canvas which they draw on.</param>
        /// <param name="drawing">The bitmap image to draw onto.</param>
        void Draw(Object Pen, Object sender, Object drawing);

        /// <summary>
        /// Method which sets the parameters of the shape so it can have sets of coordinates and sizes.
        /// </summary>
        /// <param name="list">A list of parameters for the shape, differs for each shape.</param>
       void Set(params int[] list);
    }
}
