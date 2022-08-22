using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.CartProduct
{
    public class CartProductGetDTO
    {
        public Guid ID { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
        public decimal Total { get; set; }
        public string ProductName { get; set; }

    }
}
