using SafeShop.Service.DTO.Order.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Order
{
    public class OrderGetDTO
    {
        public Guid ID { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<OrderProductGetDTO> Products { get; set; }
        public OrderDetailsGetDTO Details { get; set; }
    }
}
