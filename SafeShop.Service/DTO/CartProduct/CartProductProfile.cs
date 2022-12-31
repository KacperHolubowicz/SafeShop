using AutoMapper;

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
