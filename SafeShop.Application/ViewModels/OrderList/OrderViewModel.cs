namespace SafeShop.Application.ViewModels.OrderList
{
    public class OrderViewModel
    {
        public decimal Total { get; set; }
        public IEnumerable<OrderProductViewModel> Products { get; set; }
        public OrderDetailsViewModel Details { get; set; }
    }
}
