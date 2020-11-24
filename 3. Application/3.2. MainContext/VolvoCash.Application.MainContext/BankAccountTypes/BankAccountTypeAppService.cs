using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.BankAccountTypes;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountTypeAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.BankAccountTypes.Services
{
    public class BankAccountTypeAppService : IBankAccountTypeAppService
    {
        #region Members
        private readonly IBankAccountTypeRepository _bankAccountTypeRepository;
        #endregion

        #region Constructor
        public BankAccountTypeAppService(IBankAccountTypeRepository bankAccountTypeRepository)
        {
            _bankAccountTypeRepository = bankAccountTypeRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<BankAccountTypeDTO>> GetBankAccountTypes(bool onlyActive)
        {
            var bankAccountTypes = await _bankAccountTypeRepository.FilterAsync(filter: bat => !onlyActive || bat.Status == Status.Active);
            return bankAccountTypes.ProjectedAsCollection<BankAccountTypeDTO>();
        }

        public async Task<BankAccountTypeDTO> GetBankAccountType(int id)
        {
            var bankAccountType = await _bankAccountTypeRepository.GetAsync(id);
            return bankAccountType.ProjectedAs<BankAccountTypeDTO>();
        }

        public async Task<BankAccountTypeDTO> AddAsync(BankAccountTypeDTO bankAccountTypeDTO)
        {
            var bankAccountType = new BankAccountType(bankAccountTypeDTO.Name);
            _bankAccountTypeRepository.Add(bankAccountType);
            await _bankAccountTypeRepository.UnitOfWork.CommitAsync();
            return bankAccountType.ProjectedAs<BankAccountTypeDTO>();
        }

        public async Task<BankAccountTypeDTO> ModifyAsync(BankAccountTypeDTO bankAccountTypeDTO)
        {
            var bankAccountType = await _bankAccountTypeRepository.GetAsync(bankAccountTypeDTO.Id);
            bankAccountType.Name = bankAccountTypeDTO.Name;
            await _bankAccountTypeRepository.UnitOfWork.CommitAsync();
            return bankAccountTypeDTO;
        }

        public async Task Delete(int id)
        {
            var bankAccountType = await _bankAccountTypeRepository.GetAsync(id);
            bankAccountType.ArchiveAt = DateTime.Now;
            bankAccountType.Status = Status.Inactive;
            _bankAccountTypeRepository.Modify(bankAccountType);
            await _bankAccountTypeRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _bankAccountTypeRepository.Dispose();
        }
        #endregion
    }
}
