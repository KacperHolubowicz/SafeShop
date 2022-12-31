namespace SafeShop.Service.DTO.Payment
{
    public class PaymentPostDTO
    {
        public IEnumerable<PaymentItemPostDTO> Items { get; set; }
    }
}
