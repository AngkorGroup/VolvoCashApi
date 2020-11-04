using System;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.Cashiers.Services
{
    public interface ICashierAppService : IService<Cashier, CashierDTO>, IDisposable
    {
    }
}
