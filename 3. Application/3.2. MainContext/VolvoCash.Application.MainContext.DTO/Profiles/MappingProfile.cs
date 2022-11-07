using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Mapping, MappingDTO>().PreserveReferences();
        }
    }
}
