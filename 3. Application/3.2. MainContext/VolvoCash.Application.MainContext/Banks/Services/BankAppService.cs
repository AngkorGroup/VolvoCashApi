using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Banks;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Banks.Services
{
    public class BankAppService :  IBankAppService
    {
        #region Members
        private readonly IBankRepository _bankRepository;
        #endregion

        #region Constructor
        public BankAppService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<BankDTO>> GetBanks(bool onlyActive)
        {
            var banks =  await _bankRepository.FilterAsync(filter: b=> !onlyActive || b.Status == Status.Active);
            return banks.ProjectedAsCollection<BankDTO>();
        }

        public async Task<BankDTO> GetBank(int id)
        {
            var bank = await _bankRepository.GetAsync(id);
            return bank.ProjectedAs<BankDTO>();
        }

        public async Task<BankDTO> AddAsync(BankDTO bankDTO)
        {           
            var bank = new Bank(bankDTO.Name, bankDTO.Abbreviation, bankDTO.TPCode);
            _bankRepository.Add(bank);
            await _bankRepository.UnitOfWork.CommitAsync();
            return bank.ProjectedAs<BankDTO>();
        }

        public async Task<BankDTO> ModifyAsync(BankDTO bankDTO)
        {
            var bank = await _bankRepository.GetAsync(bankDTO.Id);
            bank.Name = bankDTO.Name;
            bank.Abbreviation = bankDTO.Abbreviation;
            bank.TPCode = bankDTO.TPCode;
            await _bankRepository.UnitOfWork.CommitAsync();
            return bankDTO;
        }

        public async Task Delete(int id)
        {
            var bank = await _bankRepository.GetAsync(id);
            bank.ArchiveAt = DateTime.Now;
            bank.Status = Status.Inactive;
            _bankRepository.Modify(bank);
            await _bankRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _bankRepository.Dispose();
        }
        #endregion
    }
}
