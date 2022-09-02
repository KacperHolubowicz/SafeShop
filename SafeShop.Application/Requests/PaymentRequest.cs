namespace SafeShop.Application.Requests
{
    public class PaymentRequest
    {
        public IEnumerable<PaymentItemRequest> Items { get; set; }
    }
}
