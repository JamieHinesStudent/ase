using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    public interface IShape
    {
       void Draw(Object sender, Object drawing);
       void Set(params int[] list);
    }
}
