using System;
using System.Threading.Tasks;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using System.Collections.Generic;

namespace VolvoCash.Application.MainContext.Cashiers.Services
{
    public interface ICashierAppService : IDisposable
    {
        #region ApiWeb
        Task<CashierDTO> GetByUserId(int userId);
        Task<List<CashierDTO>> GetCashiers(bool onlyActive);
        Task<CashierDTO> AddAsync(CashierDTO cashierDTO);
        Task<CashierDTO> ModifyAsync(CashierDTO cashierDTO);
        Task Delete(int id);   
        #endregion
    }
}
