using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Sectors;
using VolvoCash.Domain.MainContext.Aggregates.SectorAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class SectorProfile : Profile
    {
        public SectorProfile()
        {
            CreateMap<Sector, SectorDTO>().PreserveReferences();
        }
    }
}
