using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    abstract class Shape : IShape
    {
        protected int x, y; //not I could use c# properties for this
        public Shape()
        {
            x = y = 100;
        }


        public Shape(int x, int y)
        { 
            this.x = x; 
            this.y = y; 
        }

        //the three methods below are from the Shapes interface
        //here we are passing on the obligation to implement them to the derived classes by declaring them as abstract
        public abstract void Draw(Object sender, Object drawing); //any derrived class must implement this method

        //set is declared as virtual so it can be overridden by a more specific child version
        //but is here so it can be called by that child version to do the generic stuff
        //note the use of the param keyword to provide a variable parameter list to cope with some shapes having more setup information than others
        public virtual void Set(params int[] list)
        { 
            this.x = list[0];
            this.y = list[1];
        }


        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }


    }
}
