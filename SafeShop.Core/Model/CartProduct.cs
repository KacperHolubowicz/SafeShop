namespace SafeShop.Core.Model
{
    public class CartProduct
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; } = 1;
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public decimal Total { get; set; } = decimal.Zero;
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
