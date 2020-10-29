using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDTO>().PreserveReferences();
        }
    }
}
