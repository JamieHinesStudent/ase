﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    /// <summary>
    /// Class which builds the shapes. Hides the construction of the shapes.
    /// </summary>
    class ShapeFactory
    {

        public Shape getShape(String shapeType)
        {
            shapeType = shapeType.ToUpper().Trim(); //yoi could argue that you want a specific word string to create an object but I'm allowing any case combination
            
           
            if (shapeType.Equals("CIRCLE")){return new Circle();}            //Builds a circle
            else if (shapeType.Equals("RECTANGLE")){return new Rectangle();} //Builds a rectangle
            else if (shapeType.Equals("TRIANGLE")){return new Triangle ();}  //Builds a triangle
            else
            {
                //if we get here then what has been passed in is inkown so throw an appropriate exception
                System.ArgumentException argEx = new System.ArgumentException("Factory error: "+shapeType+" does not exist");
                throw argEx;
            }

           
        }
    }
}
