using System.ComponentModel.DataAnnotations;

namespace SafeShop.Core.Model
{
    public class ShippingDetails
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public OrderDetails Order { get; set; }
        public Guid OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        [StringLength(maximumLength: 6, MinimumLength = 6)]
        [RegularExpression(@"\d{2}-\d{3}$")]
        public string ZipCode { get; set; }
    }
}