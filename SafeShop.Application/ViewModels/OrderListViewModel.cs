using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class OrderListViewModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Data zamówienia")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Suma")]
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
