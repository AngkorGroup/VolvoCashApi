using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class MappingHeaderProfile : Profile
    {
        public MappingHeaderProfile()
        {
            CreateMap<MappingHeader, MappingHeaderDTO>().PreserveReferences();
        }
    }
}
