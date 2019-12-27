using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase
{
    /// <summary>
    /// Custom defined exception which checks file types.
    /// </summary>
    [Serializable]
    class InvalidFileTypeException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public InvalidFileTypeException()
        {

        }

        /// <summary>
        /// Handler for the exception.
        /// </summary>
        /// <param name="fileName">File name that was passed.</param>
        public InvalidFileTypeException(string fileName)
        : base(String.Format("File: {0}", fileName))
        {

        }
    }
}
