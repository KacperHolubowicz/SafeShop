namespace SafeShop.Application.ViewModels
{
    public class CartViewModel
    {
        public Guid ID { get; set; }
        public IEnumerable<CartProductViewModel> Products { get; set; }
    }
}
