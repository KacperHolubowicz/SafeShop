namespace SafeShop.Service.Exceptions
{
    public class DuplicatedValueException : Exception
    {
        public DuplicatedValueException(string message) : base(message) {}
        public DuplicatedValueException() : base("A duplication of values has occured.") {}
    }
}
