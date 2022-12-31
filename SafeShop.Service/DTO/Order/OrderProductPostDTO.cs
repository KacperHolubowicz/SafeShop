using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.Order
{
    public class OrderProductPostDTO
    {
        [Range(1, 10, ErrorMessage = "Prosimy dodawać między 1 a 10 produktów")]
        public int Quantity { get; set; }

        [Range(0, 10000, ErrorMessage = "Prosimy dokonywać zakupów na maksymalnie 10000 zł")]
        public decimal Total { get; set; }
        public Guid ProductID { get; set; }
    }
}
