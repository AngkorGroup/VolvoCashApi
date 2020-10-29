using AutoMapper;
using VolvoCash.Application.MainContext.DTO.CardBatches;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class CardBatchProfile : Profile
    {
        public CardBatchProfile()
        {
            CreateMap<CardBatch, CardBatchDTO>()
                .ForMember(dto => dto.ExpiresAt, cb => cb.MapFrom(cb => cb.Batch.ExpiresAt))
                .ForMember(dto => dto.ExpiresAtExtent, cb => cb.MapFrom(cb => cb.Batch.ExpiresAtExtent))
                .PreserveReferences();
        }
    }
}
