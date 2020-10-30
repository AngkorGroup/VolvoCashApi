using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class BatchRepository : Repository<Batch, MainDbContext>, IBatchRepository
    {
        #region Constructor
        public BatchRepository(MainDbContext dbContext,
                               ILogger<Repository<Batch, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
