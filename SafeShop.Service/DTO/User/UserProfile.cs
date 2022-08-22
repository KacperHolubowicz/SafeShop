using AutoMapper;
using SafeShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.User
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<Core.Model.User, UserGetDTO>();
            CreateMap<UserPostDTO, Core.Model.User>();
            CreateMap<UserPutDTO, Core.Model.User>();
        }
    }
}
