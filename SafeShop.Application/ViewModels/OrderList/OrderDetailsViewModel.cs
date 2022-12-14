namespace SafeShop.Application.ViewModels.OrderList
{
    public class OrderDetailsViewModel
    {
        public Guid ID { get; set; }
        public string Status { get; set; }
        public ShippingDetailsViewModel Shipping { get; set; }
        public BillingDetailsViewModel Billing { get; set; }
    }
}