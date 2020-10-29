using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Transfers;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class TransferProfile : Profile
    {
        public TransferProfile()
        {
            CreateMap<Transfer, TransferDTO>().PreserveReferences();
            CreateMap<Transfer, TransferListDTO>().PreserveReferences();
        }
    }
}
