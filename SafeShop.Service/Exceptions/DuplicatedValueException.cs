using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Exceptions
{
    public class DuplicatedValueException : Exception
    {
        public DuplicatedValueException(string message) : base(message) {}
        public DuplicatedValueException() : base("A duplication of values has occured.") {}
    }
}
