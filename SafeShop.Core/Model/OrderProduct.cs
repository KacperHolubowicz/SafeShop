namespace SafeShop.Core.Model
{
    public class OrderProduct
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; } = 0;
        public decimal Total { get; set; } = decimal.Zero;
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
