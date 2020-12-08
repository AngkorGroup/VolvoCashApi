using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class BatchMovementProfile : Profile
    {
        public BatchMovementProfile()
        {
            CreateMap<BatchMovement, BatchMovementDTO>().PreserveReferences();
        }
    }
}
