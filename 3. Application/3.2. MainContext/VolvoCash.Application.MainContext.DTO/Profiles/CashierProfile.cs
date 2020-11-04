using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class CashierProfile : Profile
    {
        public CashierProfile()
        {
            CreateMap<Cashier, CashierDTO>().PreserveReferences();
            CreateMap<CashierDTO, Cashier>().PreserveReferences();
        }
    }
}
