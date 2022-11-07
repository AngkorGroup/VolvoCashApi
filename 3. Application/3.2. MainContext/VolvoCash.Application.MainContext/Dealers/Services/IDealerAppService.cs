using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Dealers;

namespace VolvoCash.Application.MainContext.Dealers.Services
{
    public interface IDealerAppService : IDisposable
    {
        #region ApiWeb Public Methods
        Task<List<DealerDTO>> GetDealers(bool onlyActive);
        Task<List<DealerDTO>> GetDealers(string query, int maxRecords);
        Task<List<CashierDTO>> GetDealerCashiers(int id, bool onlyActive);
        Task<List<ChargeListDTO>> GetDealerCharges(int id, DateTime? beginDate, DateTime? endDate, int? cashierId, List<int> cardTypes);
        Task<DealerDTO> AddAsync(DealerDTO dealerDTO);
        Task<DealerDTO> ModifyAsync(DealerDTO dealerDTO);
        Task Delete(int id);
        Task<List<BankAccountDTO>> GetBankAccounts(int id, bool onlyActive);
        #endregion
    }
}
