namespace SafeShop.Core.Model
{
    public class Order
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Total { get; set; } = decimal.Zero;
        public string Status { get; set; } = string.Empty;
        public IEnumerable<OrderProduct> Products { get; set; }
        public User User { get; set; }
    }
}
