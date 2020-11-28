using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Currency, CurrencyDTO>().PreserveReferences();
        }
    }
}
