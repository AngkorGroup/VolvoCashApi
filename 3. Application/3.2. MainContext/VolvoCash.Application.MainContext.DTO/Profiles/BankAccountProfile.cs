using AutoMapper;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<BankAccount, BankAccountDTO>().PreserveReferences();
        }
    }
}
