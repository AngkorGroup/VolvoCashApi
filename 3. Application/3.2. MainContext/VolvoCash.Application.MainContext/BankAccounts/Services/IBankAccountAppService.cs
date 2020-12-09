using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.BankAccounts;

namespace VolvoCash.Application.MainContext.BankAccounts.Services
{
    public interface IBankAccountAppService : IDisposable
    {
        #region ApiWeb
        Task<List<BankAccountDTO>> GetBankAccounts(bool onlyActive);
        Task<List<BankAccountDTO>> GetBankAccountsForVolvo(bool onlyActive);
        Task<BankAccountDTO> GetBankAccount(int id);
        Task<BankAccountDTO> AddAsync(BankAccountDTO bankAccountDTO);
        Task<BankAccountDTO> ModifyAsync(BankAccountDTO bankAccountDTO);
        Task Delete(int id);
        #endregion
    }
}
