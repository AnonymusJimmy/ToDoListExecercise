using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions.Exceptions
{
    public class AlreadyExistException : CustomExeptions
    {
        public override string Message
        {
            get
            {
                return "Attention, Item already exists.";
            }
        }
    }
}
