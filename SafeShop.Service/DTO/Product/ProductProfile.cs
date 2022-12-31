using AutoMapper;

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
