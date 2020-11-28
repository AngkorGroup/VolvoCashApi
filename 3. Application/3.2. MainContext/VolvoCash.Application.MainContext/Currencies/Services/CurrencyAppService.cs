using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Currencies.Services
{
    public class CurrencyAppService : ICurrencyAppService
    {
        #region Members
        private readonly ICurrencyRepository _currencyRepository;
        #endregion

        #region Constructor
        public CurrencyAppService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<CurrencyDTO>> GetCurrencies(bool onlyActive)
        {
            var currencies = await _currencyRepository.FilterAsync(filter: c => !onlyActive || c.Status == Status.Active);
            return currencies.ProjectedAsCollection<CurrencyDTO>();
        }

        public async Task<CurrencyDTO> GetCurrency(int id)
        {
            var currency = await _currencyRepository.GetAsync(id);
            return currency.ProjectedAs<CurrencyDTO>();
        }

        public async Task<CurrencyDTO> AddAsync(CurrencyDTO currencyDTO)
        {
            var currency = new Currency(
                currencyDTO.Name,
                currencyDTO.Abbreviation,
                currencyDTO.Symbol,
                currencyDTO.TPCode
            );
            _currencyRepository.Add(currency);
            await _currencyRepository.UnitOfWork.CommitAsync();
            return currency.ProjectedAs<CurrencyDTO>();
        }

        public async Task<CurrencyDTO> ModifyAsync(CurrencyDTO currencyDTO)
        {
            var currency = await _currencyRepository.GetAsync(currencyDTO.Id);
            currency.Name = currencyDTO.Name;
            currency.Abbreviation = currencyDTO.Abbreviation;
            currency.Symbol = currencyDTO.Symbol;
            currency.TPCode = currencyDTO.TPCode;
            await _currencyRepository.UnitOfWork.CommitAsync();
            return currencyDTO;
        }

        public async Task Delete(int id)
        {
            var currency = await _currencyRepository.GetAsync(id);
            currency.ArchiveAt = DateTime.Now;
            currency.Status = Status.Inactive;
            _currencyRepository.Modify(currency);
            await _currencyRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _currencyRepository.Dispose();
        }
        #endregion
    }
}
