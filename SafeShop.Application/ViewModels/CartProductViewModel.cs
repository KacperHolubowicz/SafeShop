namespace SafeShop.Application.ViewModels
{
    public class CartProductViewModel
    {
        public Guid ID { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string ProductName { get; set; }
    }
}
