using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.BankAccounts.Services
{
    public class BankAccountAppService : IBankAccountAppService
    {
        #region Members
        private readonly IBankAccountRepository _bankAccountRepository;

        #endregion

        #region Constructor
        public BankAccountAppService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<BankAccountDTO>> GetBankAccounts(bool onlyActive)
        {
            var bankAccounts = await _bankAccountRepository.FilterAsync(
                filter: b => !onlyActive || b.Status == Status.Active,
                includeProperties: "Bank,BankAccountType,Currency");
            return bankAccounts.ProjectedAsCollection<BankAccountDTO>();
        }

        public async Task<BankAccountDTO> GetBankAccount(int id)
        {
            var bankAccount = await _bankAccountRepository.GetAsync(id);
            return bankAccount.ProjectedAs<BankAccountDTO>();
        }

        public async Task<BankAccountDTO> AddAsync(BankAccountDTO bankAccountDTO)
        {
            var bankAccount = new BankAccount(
                bankAccountDTO.Account,
                bankAccountDTO.CCI,
                bankAccountDTO.IsDefault,
                bankAccountDTO.BankAccountTypeId,
                bankAccountDTO.CurrencyId,
                bankAccountDTO.BankId,
                bankAccountDTO.DealerId
            );
            _bankAccountRepository.Add(bankAccount);
            await _bankAccountRepository.UnitOfWork.CommitAsync();
            return bankAccount.ProjectedAs<BankAccountDTO>();
        }

        public async Task<BankAccountDTO> ModifyAsync(BankAccountDTO bankAccountDTO)
        {
            var bankAccount = await _bankAccountRepository.GetAsync(bankAccountDTO.Id);

            bankAccount.Account = bankAccountDTO.Account;
            bankAccount.CCI = bankAccountDTO.CCI;
            bankAccount.IsDefault = bankAccountDTO.IsDefault;
            bankAccount.BankAccountTypeId = bankAccountDTO.BankAccountTypeId;
            bankAccount.CurrencyId = bankAccountDTO.CurrencyId;
            bankAccount.BankId = bankAccountDTO.BankId;
            bankAccount.DealerId = bankAccountDTO.DealerId;

            await _bankAccountRepository.UnitOfWork.CommitAsync();
            return bankAccountDTO;
        }

        public async Task Delete(int id)
        {
            var bankAccount = await _bankAccountRepository.GetAsync(id);
            bankAccount.IsDefault = false;
            bankAccount.ArchiveAt = DateTime.Now;
            bankAccount.Status = Status.Inactive;
            _bankAccountRepository.Modify(bankAccount);
            await _bankAccountRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _bankAccountRepository.Dispose();
        }
        #endregion
    }
}
