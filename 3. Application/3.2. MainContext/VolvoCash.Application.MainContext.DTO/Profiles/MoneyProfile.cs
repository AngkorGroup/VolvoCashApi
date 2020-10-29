using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class MoneyProfile : Profile
    {
        public MoneyProfile()
        {
            CreateMap<Money, MoneyDTO>().PreserveReferences();
        }
    }
}
