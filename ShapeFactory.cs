using System;

namespace ase
{
    /// <summary>
    /// Class which builds the shapes. Hides the complexity of the construction of the shapes.
    /// </summary>
    class ShapeFactory
    {

        /// <summary>
        /// Constructs the shape object based on the type passed into it.
        /// </summary>
        /// <param name="shapeType">The shape to be constructed.</param>
        /// <returns>The newly created shape object.</returns>
        public Shape getShape(String shapeType)
        {
            shapeType = shapeType.ToUpper().Trim(); //Removes whitespaces and converts to uppercase
            
           
            if (shapeType.Equals("CIRCLE")){return new Circle();}            //Builds a circle
            else if (shapeType.Equals("RECTANGLE")){return new Rectangle();} //Builds a rectangle
            else if (shapeType.Equals("TRIANGLE")){return new Triangle ();}  //Builds a triangle
            else
            {
                //Shape doesn't exist
                System.ArgumentException argEx = new System.ArgumentException("Factory error: "+shapeType+" does not exist");
                throw argEx;
            }          
        }
    }
}
