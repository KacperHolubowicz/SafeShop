namespace SafeShop.Service.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }

        public ResourceNotFoundException() : base("Resource could not be found") { }
    }
}
