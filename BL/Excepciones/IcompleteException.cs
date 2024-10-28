using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Excepciones
{
    public class IcompleteException : Exception
    {
        public IcompleteException(string message) : base(message)
        {
        }
    }
}
