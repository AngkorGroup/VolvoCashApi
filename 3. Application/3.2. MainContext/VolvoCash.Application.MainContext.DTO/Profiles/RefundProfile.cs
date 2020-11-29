using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Refunds;
using VolvoCash.Domain.MainContext.Aggregates.RefundAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class RefundProfile : Profile
    {
        public RefundProfile()
        {
            CreateMap<Refund, RefundDTO>().PreserveReferences();
        }
    }
}
