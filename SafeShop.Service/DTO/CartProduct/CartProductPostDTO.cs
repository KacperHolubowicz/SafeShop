﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.CartProduct
{
    public class CartProductPostDTO
    {
        public int Quantity { get; set; }
        public Guid ProductID { get; set; }
        public Guid? CartID { get; set; }
    }
}
