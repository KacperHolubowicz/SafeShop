using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.Payment
{
    public class PaymentItemPostDTO
    {
        [MinLength(1)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Range(1, 10)]
        public int Quantity { get; set; }
    }
}
