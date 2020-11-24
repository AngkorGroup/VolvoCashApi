using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class LiquidationRepository : Repository<Liquidation, MainDbContext>, ILiquidationRepository
    {
        #region Constructor
        public LiquidationRepository(MainDbContext dbContext, ILogger<Repository<Liquidation, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
