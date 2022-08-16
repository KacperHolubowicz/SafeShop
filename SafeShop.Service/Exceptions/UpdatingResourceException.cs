using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Exceptions
{
    public class UpdatingResourceException : Exception
    {
        public UpdatingResourceException(string message) : base(message) { }
        public UpdatingResourceException() : base("Updating resource failed.") { }
    }
}
