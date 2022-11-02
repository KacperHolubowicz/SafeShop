using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Core.Model
{
    public class OrderDetails
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public Guid OrderID { get; set; }
        public Order Order { get; set; }
        public string Status { get; set; } = string.Empty;
        public ShippingDetails Shipping { get; set; }
        public BillingDetails Billing { get; set; }
    }
}
