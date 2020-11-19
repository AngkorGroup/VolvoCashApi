using System;
using System.Threading.Tasks;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.Cashiers.Services
{
    public interface ICashierAppService : IService<Cashier, CashierDTO>, IDisposable
    {
        Task Delete(int id);
        Task<CashierDTO> GetByUserId(int userId);
    }
}
