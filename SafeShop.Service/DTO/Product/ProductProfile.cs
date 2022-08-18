using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Core.Model.Product, ProductGetDTO>();
            CreateMap<Core.Model.Product, ProductGetListDTO>();
            CreateMap<ProductPostDTO, Core.Model.Product>();
            CreateMap<ProductPutDTO, Core.Model.Product>();
        }
    }
}
