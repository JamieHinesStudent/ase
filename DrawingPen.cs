using System.Drawing;

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

        public Pen Colour { get; set; } //pen object
        

        /// <summary>
        /// Returns the pen object so the colour can be found.
        /// </summary>
        /// <returns>Pen object.</returns>
        public Pen returnColour()
        {
            return Colour;
        }

        /// <summary>
        /// Function to set the colour of the pen object in the program.
        /// </summary>
        /// <param name="colourName">Colour to set</param>
        /// <returns>True if the colour was set, false if it was not.</returns>
        public bool SetColour(string colourName)
        {
            switch (colourName){
                case "RED": Colour.Color = Color.Red;  return true;
                case "BLUE": Colour.Color = Color.Blue; return true;
                case "YELLOW": Colour.Color = Color.Yellow; return true;
                case "GREEN": Colour.Color = Color.Green; return true;
                case "ORANGE": Colour.Color = Color.Orange; return true;
                case "BLACK": Colour.Color = Color.Black; return true;
                case "PINK": Colour.Color = Color.Pink; return true;
                case "WHITE": Colour.Color = Color.White; return true;
            }
            return false;

        }

        



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
            Colour = new Pen(Color.Black);
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
