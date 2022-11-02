using SafeShop.Service.DTO.Order.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Order
{
    public class OrderPostDTO
    {
        public Guid CartID { get; set; }
        public OrderDetailsPostDTO Details { get; set; }
    }
}
