using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Order.Details
{
    public class OrderDetailsPostDTO
    {
        public string Status { get; set; }
        public ShippingDetailsPostDTO Shipping { get; set; }
        public BillingDetailsPostDTO Billing { get; set; }
    }
}
