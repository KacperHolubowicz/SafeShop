namespace SafeShop.Service.DTO.Order
{
    public class OrderListDTO
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
