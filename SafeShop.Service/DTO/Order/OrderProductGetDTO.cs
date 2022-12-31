namespace SafeShop.Service.DTO.Order
{
    public class OrderProductGetDTO
    {
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string ProductName { get; set; }
    }
}
