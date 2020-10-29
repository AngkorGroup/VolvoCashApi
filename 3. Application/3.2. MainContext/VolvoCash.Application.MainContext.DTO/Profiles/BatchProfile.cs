using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class BatchProfile : Profile
    {
        public BatchProfile()
        {
            CreateMap<Batch, BatchDTO>().PreserveReferences();
        }
    }
}
