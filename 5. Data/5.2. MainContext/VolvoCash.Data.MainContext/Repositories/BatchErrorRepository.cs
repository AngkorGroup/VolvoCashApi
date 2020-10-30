using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class BatchErrorRepository : Repository<BatchError, MainDbContext>, IBatchErrorRepository
    {
        #region Constructor
        public BatchErrorRepository(MainDbContext dbContext,
                               ILogger<Repository<BatchError, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
