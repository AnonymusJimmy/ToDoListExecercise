using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions.Exceptions
{
    public class CustomExeptions : Exception
    {
        public CustomExeptions() : base() { }
        public CustomExeptions(string message) : base(message) { }
        public CustomExeptions(string message, Exception innerException) : base(message, innerException) { }
    }
}
