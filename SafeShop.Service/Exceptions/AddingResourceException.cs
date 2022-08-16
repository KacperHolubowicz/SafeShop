using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Exceptions
{
    public class AddingResourceException : Exception
    {
        public AddingResourceException(string message) : base(message) { }
        public AddingResourceException() : base("Adding resource failed.") { }
    }
}
