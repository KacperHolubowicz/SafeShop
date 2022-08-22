using SafeShop.Service.DTO.CartProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Cart
{
    public class CartPutDTO
    {
        public IEnumerable<CartProductPostDTO> Products { get; set; }
    }
}
