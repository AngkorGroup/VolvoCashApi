using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class RechargeTypeRepository : Repository<RechargeType, MainDbContext>, IRechargeTypeRepository
    {
        #region Constructor
        public RechargeTypeRepository(MainDbContext dbContext, ILogger<Repository<RechargeType, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
