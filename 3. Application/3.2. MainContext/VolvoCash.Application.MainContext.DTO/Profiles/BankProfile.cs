using AutoMapper;
using VolvoCash.Application.MainContext.DTO.Banks;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class BankProfile : Profile
    {
        public BankProfile()
        {
            CreateMap<Bank, BankDTO>().PreserveReferences();
        }
    }
}
