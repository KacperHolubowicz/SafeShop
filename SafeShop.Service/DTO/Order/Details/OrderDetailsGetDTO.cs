namespace SafeShop.Service.DTO.Order.Details
{
    public class OrderDetailsGetDTO
    {
        public Guid ID { get; set; }
        public string Status { get; set; }
        public ShippingDetailsGetDTO Shipping { get; set; }
        public BillingDetailsGetDTO Billing { get; set; }
    }
}
