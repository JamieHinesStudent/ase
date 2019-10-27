﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace ase
{
    /// <summary>
    /// Class which draws a circle shape on the screen.
    /// </summary>
    class Circle : Shape
    {
        int radius;

        /// <summary>
        /// Base constructor.
        /// </summary>
        public Circle():base()
        {

        }

        /// <summary>
        /// Circle constructor.
        /// </summary>
        /// <param name="radius">radius to draw.</param>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        public Circle(int radius, int x, int y) : base(x, y)
        {
            this.radius = radius; //radius
        }

        /// <summary>
        /// Sets the parameters for the circle so it can be drawn.
        /// </summary>
        /// <param name="list">Integer list which contains the x coordinate, y coordinate and the radius.</param>
        public override void Set(params int[] list)
        {
            base.Set(list[0], list[1]); //x and y coordinates
            radius = list[2]; //radius         
        }

        /// <summary>
        /// Draws the circle on the screen.
        /// </summary>
        /// <param name="sender">The canvas to be drawn on.</param>
        /// <param name="drawing">The image to be drawn on.</param>
        public override void Draw(Object sender, Object drawing)
        {
            //Constructs the objects
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);

            g.DrawEllipse(Pens.Black, x, y, radius * 2, radius * 2); //Draw function
            canvas.Image = image; //Sets the bitmap image to the canvas

            g.Dispose();
        }
    }
}