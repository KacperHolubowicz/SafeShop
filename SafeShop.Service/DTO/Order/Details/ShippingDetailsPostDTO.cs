using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Order.Details
{
    public class ShippingDetailsPostDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }

        [StringLength(maximumLength: 6, MinimumLength = 6)]
        [RegularExpression(@"\d{2}-\d{3}$")]
        public string ZipCode { get; set; }
    }
}
