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

            switch (shapeType)
            {
                case "CIRCLE": return new Circle();
                case "RECTANGLE": return new Rectangle();
                case "TRIANGLE": return new Triangle();
                case "POLYGON": return new Polygon();
                default:
                    System.ArgumentException argEx = new System.ArgumentException("Factory error: " + shapeType + " does not exist");
                    throw argEx;
            }
                    
        }
    }
}
