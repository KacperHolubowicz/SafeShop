namespace SafeShop.Service.Exceptions
{
    public class AddingResourceException : Exception
    {
        public AddingResourceException(string message) : base(message) { }
        public AddingResourceException() : base("Adding resource failed.") { }
    }
}
