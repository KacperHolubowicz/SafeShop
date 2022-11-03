namespace SafeShop.Application.Requests.Order
{
    public class OrderDetailsRequest
    {
        public ShippingRequest Shipping { get; set; }
        public BillingRequest Billing { get; set; }
    }
}
