namespace SafeShop.Application.Requests.Order
{
    public class ShippingRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
