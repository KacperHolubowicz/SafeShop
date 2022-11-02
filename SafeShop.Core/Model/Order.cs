namespace SafeShop.Core.Model
{
    public class Order
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Total { get; set; } = decimal.Zero;
        public IEnumerable<OrderProduct> Products { get; set; }
        public User? User { get; set; }
        public OrderDetails Details { get; set; }
    }
}
