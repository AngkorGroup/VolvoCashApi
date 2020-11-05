using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Users;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().PreserveReferences();
            CreateMap<UserDTO, User>().PreserveReferences();
        }
    }
}
