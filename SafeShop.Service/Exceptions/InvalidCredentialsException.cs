public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string message) : base(message) { }

    public InvalidCredentialsException() : base("Provided credentials are invalid") { }
}

