using AutoMapper;
using SafeShop.Core.Model;

namespace SafeShop.Service.DTO.Order.Details
{
    public class DetailsProfile : Profile
    {
        public DetailsProfile()
        {
            CreateMap<BillingDetailsPostDTO, BillingDetails>();
            CreateMap<BillingDetails, BillingDetailsGetDTO>();
            CreateMap<ShippingDetails, ShippingDetailsGetDTO>();
            CreateMap<ShippingDetailsPostDTO, ShippingDetails>();
            CreateMap<OrderDetails, OrderDetailsGetDTO>();
            CreateMap<OrderDetailsPostDTO, OrderDetails>();
        }
    }
}
