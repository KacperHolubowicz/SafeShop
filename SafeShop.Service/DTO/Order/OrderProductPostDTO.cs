using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Order
{
    public class OrderProductPostDTO
    {
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public Guid ProductID { get; set; }
    }
}
