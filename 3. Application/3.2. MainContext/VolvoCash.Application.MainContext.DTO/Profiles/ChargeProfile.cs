using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class ChargeProfile : Profile
    {
        public ChargeProfile()
        {
            CreateMap<Charge, ChargeDTO>().PreserveReferences();
        }
    }
}
