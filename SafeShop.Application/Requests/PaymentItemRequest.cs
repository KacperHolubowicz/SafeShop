namespace SafeShop.Application.Requests
{
    public class PaymentItemRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
