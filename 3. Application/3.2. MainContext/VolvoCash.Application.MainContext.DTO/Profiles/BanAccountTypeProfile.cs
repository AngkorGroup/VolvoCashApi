using AutoMapper;
using VolvoCash.Application.MainContext.DTO.BankAccountTypes;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountTypeAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class BankAccountTypeProfile : Profile
    {
        public BankAccountTypeProfile()
        {
            CreateMap<BankAccountType, BankAccountTypeDTO>().PreserveReferences();
        }
    }
}
