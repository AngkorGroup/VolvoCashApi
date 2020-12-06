using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Menus;
using VolvoCash.Domain.MainContext.Aggregates.MenuAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuDTO>().PreserveReferences();
        }
    }
}
