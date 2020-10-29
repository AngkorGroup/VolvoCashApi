using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDTO>().PreserveReferences();
            CreateMap<Card, CardListDTO>().PreserveReferences();
        }
    }
}
