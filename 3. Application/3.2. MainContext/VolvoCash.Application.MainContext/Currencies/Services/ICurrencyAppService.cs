using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Currencies;

namespace VolvoCash.Application.MainContext.Currencies.Services
{
    public interface ICurrencyAppService : IDisposable
    {
        #region ApiWeb
        Task<List<CurrencyDTO>> GetCurrencies(bool onlyActive);
        Task<CurrencyDTO> GetCurrency(int id);
        Task<CurrencyDTO> AddAsync(CurrencyDTO currencyDTO);
        Task<CurrencyDTO> ModifyAsync(CurrencyDTO currencyDTO);
        Task Delete(int id);
        #endregion
    }
}
