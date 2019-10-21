using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class DrawingPen
    {
        public int xCoordinate { get; set; }
        public int yCoordinate { get; set; }
        private int maxXCoordinate;
        private int maxYCoordinate;

        public DrawingPen(int x, int y, int maxX, int maxY)
        {
            xCoordinate = x;
            yCoordinate = y;
            maxXCoordinate = maxX;
            maxYCoordinate = maxY;
        }

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
