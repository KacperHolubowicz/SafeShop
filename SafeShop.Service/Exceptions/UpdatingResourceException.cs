namespace SafeShop.Service.Exceptions
{
    public class UpdatingResourceException : Exception
    {
        public UpdatingResourceException(string message) : base(message) { }
        public UpdatingResourceException() : base("Updating resource failed.") { }
    }
}
