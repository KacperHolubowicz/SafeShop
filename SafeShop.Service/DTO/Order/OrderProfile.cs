using AutoMapper;
using SafeShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Order
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderProduct, OrderProductGetDTO>();
            CreateMap<Core.Model.Order, OrderGetDTO>();
            CreateMap<Core.Model.Order, OrderListDTO>()
                .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Details.Status));
            CreateMap<Core.Model.CartProduct, Core.Model.OrderProduct>();
        }
    }
}
