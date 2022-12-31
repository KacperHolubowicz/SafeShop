using AutoMapper;

namespace SafeShop.Service.DTO.User
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<Core.Model.User, UserGetDTO>();
            CreateMap<UserPutDTO, Core.Model.User>();
        }
    }
}
