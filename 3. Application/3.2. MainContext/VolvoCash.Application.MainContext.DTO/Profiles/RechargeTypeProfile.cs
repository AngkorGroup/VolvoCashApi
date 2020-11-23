using AutoMapper;
using VolvoCash.Application.MainContext.DTO.RechargeTypes;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class RechargeTypeProfile : Profile
    {
        public RechargeTypeProfile()
        {
            CreateMap<RechargeType, RechargeTypeDTO>().PreserveReferences();
        }
    }
}
