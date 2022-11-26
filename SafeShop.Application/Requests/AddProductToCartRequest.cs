namespace SafeShop.Application.Requests
{
    public class AddProductToCartRequest
    {
        public int Quantity { get; set; }
        public Guid ProductID { get; set; }
        public Guid? CartID { get; set; }
        public Guid? UserID { get; set; } = Guid.Empty;
    }
}
