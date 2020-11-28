using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.BankAccountTypes;

namespace VolvoCash.Application.MainContext.BankAccountTypes.Services
{
    public interface IBankAccountTypeAppService : IDisposable
    {
        #region ApiWeb
        Task<List<BankAccountTypeDTO>> GetBankAccountTypes(bool onlyActive);
        Task<BankAccountTypeDTO> GetBankAccountType(int id);
        Task<BankAccountTypeDTO> AddAsync(BankAccountTypeDTO bankAccountTypeDTO);
        Task<BankAccountTypeDTO> ModifyAsync(BankAccountTypeDTO bankAccountTypeDTO);
        Task Delete(int id);
        #endregion
    }
}
