using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.BankAccounts.Services
{
    public class BankAccountAppService : IBankAccountAppService
    {
        #region Members
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public BankAccountAppService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _resources = LocalizationFactory.CreateLocalResources();
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

        public async Task<List<BankAccountDTO>> GetBankAccountsForVolvo(bool onlyActive)
        {
            var bankAccounts = await _bankAccountRepository.FilterAsync(
                filter: b => (!onlyActive || b.Status == Status.Active) && b.DealerId == null,
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
            var bankAccounts = await _bankAccountRepository.FilterAsync(
                filter: ba => (ba.Account == bankAccountDTO.Account || ba.CCI == bankAccountDTO.CCI)
                    && ba.Status == Status.Active
                );

            if (bankAccounts.Any())
            {
                var bankAccountAlreadyExistsMessage = _resources.GetStringResource(LocalizationKeys.Application.exception_BankAccountAlreadyExists);
                bankAccountAlreadyExistsMessage = string.Format(bankAccountAlreadyExistsMessage, bankAccountDTO.Account, bankAccountDTO.CCI);
                throw new InvalidOperationException(bankAccountAlreadyExistsMessage);
            }

            var bankAccount = new BankAccount(
                bankAccountDTO.Account,
                bankAccountDTO.CCI,
                bankAccountDTO.IsDefault,
                bankAccountDTO.BankAccountTypeId,
                bankAccountDTO.CurrencyId,
                bankAccountDTO.BankId,
                bankAccountDTO.DealerId
            );

            if (bankAccount.IsDefault)
            {
                var bankAccountsToUpdate = await _bankAccountRepository.FilterAsync(
                    filter: ba => ba.BankId == bankAccount.BankId
                        && ba.CurrencyId == bankAccount.CurrencyId
                        && ba.DealerId == bankAccount.DealerId
                        && ba.Status == Status.Active
                    );

                if (bankAccountsToUpdate.Any())
                {
                    foreach (var bankAccountToUpdate in bankAccountsToUpdate)
                    {
                        bankAccountToUpdate.IsDefault = false;
                    }
                }
            }

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

            if (bankAccount.IsDefault)
            {
                var bankAccountsToUpdate = await _bankAccountRepository.FilterAsync(
                    filter: ba => ba.BankId == bankAccount.BankId
                        && ba.CurrencyId == bankAccount.CurrencyId
                        && ba.DealerId == bankAccount.DealerId
                        && ba.Id != bankAccount.Id
                        && ba.Status == Status.Active
                    );

                if (bankAccountsToUpdate.Any())
                {
                    foreach (var bankAccountToUpdate in bankAccountsToUpdate)
                    {
                        bankAccountToUpdate.IsDefault = false;
                    }
                }
            }

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
