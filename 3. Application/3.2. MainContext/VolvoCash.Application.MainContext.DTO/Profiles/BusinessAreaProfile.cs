using AutoMapper;
using VolvoCash.Application.MainContext.DTO.BusinessAreas;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class BusinessAreaProfile : Profile
    {
        public BusinessAreaProfile()
        {
            CreateMap<BusinessArea, BusinessAreaDTO>().PreserveReferences();
        }
    }
}
