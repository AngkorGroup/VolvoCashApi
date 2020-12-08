using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Roles;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class RoleAdminProfile : Profile
    {
        public RoleAdminProfile()
        {
            CreateMap<RoleAdmin, RoleAdminDTO>().PreserveReferences();
        }
    }
}
