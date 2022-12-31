namespace SafeShop.Service.DTO.Order.Details
{
    public class OrderDetailsPostDTO
    {
        public ShippingDetailsPostDTO Shipping { get; set; }
        public BillingDetailsPostDTO Billing { get; set; }
    }
}
