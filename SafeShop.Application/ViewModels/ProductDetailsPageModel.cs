namespace SafeShop.Application.ViewModels
{
    public class ProductDetailsPageModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool IsInCart { get; set; }
    }
}
