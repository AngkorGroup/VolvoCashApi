using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg
{
    public interface ILiquidationRepository : IRepository<Liquidation>
    {
        Task<IEnumerable<Liquidation>> GetLiquidations(DateTime beginDate, DateTime endDate, LiquidationStatus liquidationStatus);
        Task<Liquidation> GetLiquidation(int id);
        Task<Liquidation> GetLiquidationForScheduled(int id);
        Task<Liquidation> GetLiquidationWithCharges(int id);
    }
}
