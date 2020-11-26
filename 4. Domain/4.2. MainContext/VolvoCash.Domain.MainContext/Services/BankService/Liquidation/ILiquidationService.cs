using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

namespace VolvoCash.Domain.MainContext.Services.BankService
{
    public interface ILiquidationService
    {
        Task<IEnumerable<Liquidation>> GenerateLiquidationsAsync();
    }
}
