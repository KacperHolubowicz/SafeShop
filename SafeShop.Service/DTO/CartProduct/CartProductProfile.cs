using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.CartProduct
{
    public class CartProductProfile : Profile
    {
        public CartProductProfile()
        {
            CreateMap<Core.Model.Product, CartProductGetDTO>();
        }
    }
}
