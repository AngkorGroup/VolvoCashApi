using AutoMapper;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class CardTypeProfile : Profile
    {
        public CardTypeProfile()
        {
            CreateMap<CardType, CardTypeDTO>().PreserveReferences();
            CreateMap<CardTypeDTO, CardType>().PreserveReferences();
            CreateMap<CardTypeSummary, CardTypeSummaryDTO>().PreserveReferences();
        }
    }
}
