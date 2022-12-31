namespace SafeShop.Service.DTO.CartProduct
{
    public class CartProductGetDTO
    {
        public Guid ID { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
        public decimal Total { get; set; }
        public string ProductName { get; set; }

    }
}
