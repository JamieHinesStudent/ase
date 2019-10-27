namespace ase
{
    ///<summary>
    ///DrawingPen class. It holds all the information about the drawing pen. Such as it's x and y corrdinates.
    ///</summary>
    class DrawingPen
    {
        public int xCoordinate { get; set; } //Current x coordinate of the pen
        public int yCoordinate { get; set; } //Current y coordinate of the pen
        private readonly int maxXCoordinate; //Max width of the canvas
        private readonly int maxYCoordinate; //Max height of the canvas

        /// <summary>
        /// Constructor which takes in the initial x,y coordinates and the x,y size of the screen.
        /// </summary>
        /// <param name="x">x coordinate of the pen.</param>
        /// <param name="y">y coordinate of the pen.</param>
        /// <param name="maxX">max width of the canvas.</param>
        /// <param name="maxY">max height of the canvas.</param>
        public DrawingPen(int x, int y, int maxX, int maxY)
        {
            xCoordinate = x;
            yCoordinate = y;
            maxXCoordinate = maxX;
            maxYCoordinate = maxY;
        }
        
        /// <summary>
        /// Function to check if an x/y coordinate will go out of bounds on the screen.
        /// </summary>
        /// <param name="x">x coordinate to check.</param>
        /// <param name="y">y coordinate to check.</param>
        /// <returns>Boolean value. False if the x/y coordinate is off the canvas and true if it's not.</returns>
        public bool CheckDimensions(int x, int y)
        {
            if (x < 0 || x > maxXCoordinate || y < 0 || y > maxYCoordinate)
            {
                return false; //x or y coordinate is out of bounds
            }
            else
            {
                return true; //x and y is within the bounds
            }
        }
    }
}
