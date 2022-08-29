namespace SafeShop.Core.Model
{
    public class Cart
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; }
        public IEnumerable<CartProduct> Products { get; set; }
        public User? User { get; set; }
        public Guid? UserID { get; set; }
    }
}
