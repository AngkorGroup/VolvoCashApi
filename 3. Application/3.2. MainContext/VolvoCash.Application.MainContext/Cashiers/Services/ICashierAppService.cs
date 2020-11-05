using System;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using System.Threading.Tasks;

namespace VolvoCash.Application.MainContext.Cashiers.Services
{
    public interface ICashierAppService : IService<Cashier, CashierDTO>, IDisposable
    {
        Task Delete(int id);
    }
}
