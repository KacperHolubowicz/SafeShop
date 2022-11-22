using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string message) : base(message) { }

    public InvalidCredentialsException() : base("Provided credentials are invalid") { }
}

