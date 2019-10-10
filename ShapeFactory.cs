using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    class ShapeFactory
    {

        public static IShape GetShape<T>() where T : IShape
        {
            return Activator.CreateInstance<T>();
        }
    }
}
