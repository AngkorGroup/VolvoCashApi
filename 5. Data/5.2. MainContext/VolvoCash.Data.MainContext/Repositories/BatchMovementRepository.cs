using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class BatchMovementRepository : Repository<BatchMovement, MainDbContext>, IBatchMovementRepository
    {
        #region Constructor
        public BatchMovementRepository(MainDbContext dbContext,
                                  ILogger<Repository<BatchMovement, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
