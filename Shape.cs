using System;

namespace ase
{
    /// <summary>
    /// Base class shape which implements the interface IShape.
    /// </summary>
    abstract class Shape : IShape
    {
        protected int x, y; //x and y coordinates

        /// <summary>
        /// Base constructor.
        /// </summary>
        public Shape()
        {
            x = y = 0; //Sets default values of x and y coordinates
        }

        /// <summary>
        /// Constructor for the shape class.
        /// </summary>
        /// <param name="x">The current x coordinate for the shape.</param>
        /// <param name="y">The current y coordinate for the shape.</param>
        public Shape(int x, int y)
        { 
            this.x = x; //set x coordinate
            this.y = y; //set y coordinate
        }

        /// <summary>
        /// Abstract methods that the shape classes implement to draw shapes.
        /// </summary>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        public abstract void Draw(Object sender, Object drawing);

        /// <summary>
        /// Virtual set method which can be overridden by more specific child versions. Sets the parameters for the shape.
        /// </summary>
        /// <param name="list">The parameters to pass in, varies depending on the ovveride.</param>
        public virtual void Set(params int[] list)
        { 
            this.x = list[0]; //x coordinate set
            this.y = list[1]; //y coordinate set
        }

        /// <summary>
        /// Prints out the contents of the shape
        /// </summary>
        /// <returns>a string represnting the object</returns>
        public override string ToString()
        {
            return base.ToString();
        }


    }
}
