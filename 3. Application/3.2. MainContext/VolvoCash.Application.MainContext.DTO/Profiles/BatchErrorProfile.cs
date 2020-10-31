using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class BatchErrorProfile : Profile
    {
        public BatchErrorProfile()
        {
            CreateMap<BatchError, BatchErrorDTO>().PreserveReferences();
        }
    }
}
