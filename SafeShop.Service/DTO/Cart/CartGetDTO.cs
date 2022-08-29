using SafeShop.Service.DTO.CartProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Cart
{
    public class CartGetDTO
    {
        public Guid ID { get; set; }
        public IEnumerable<CartProductGetDTO> Products { get; set; }
    }
}
