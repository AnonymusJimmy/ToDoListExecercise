using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions.Exceptions
{
    public class NotExistException : CustomExeptions
    {

        public NotExistException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return "Error, item doesn't exist";
            }
        }
    }
}
