using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    ///<summary>
    ///DrawingPen class. It holds all the information about the drawing pen. Such as it's x and y corrdinates.
    ///</summary>
    class DrawingPen
    {
        public int xCoordinate { get; set; }
        public int yCoordinate { get; set; }
        private int maxXCoordinate;
        private int maxYCoordinate;

        ///<summary>
        ///Constructor which takes in the initial x,y coordinates and the x,y size of the screen.
        ///</summary>
        public DrawingPen(int x, int y, int maxX, int maxY)
        {
            xCoordinate = x;
            yCoordinate = y;
            maxXCoordinate = maxX;
            maxYCoordinate = maxY;
        }

        ///<summary>
        ///Function to check if an x/y coordinate will go out of bounds on the screen.
        ///</summary>
        ///<returns>
        ///Boolean value. False if the x/y coordinate is off the canvas and true if it's not.
        ///</returns>
        public bool checkDimensions(int x, int y)
        {
            if (x > maxXCoordinate || y > maxYCoordinate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
