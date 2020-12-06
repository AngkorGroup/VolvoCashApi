using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Roles;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class RoleMenuProfile : Profile
    {
        public RoleMenuProfile()
        {
            CreateMap<RoleMenu, RoleMenuDTO>().PreserveReferences();
        }
    }
}
