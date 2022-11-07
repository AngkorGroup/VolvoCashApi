using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Cashiers;

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
