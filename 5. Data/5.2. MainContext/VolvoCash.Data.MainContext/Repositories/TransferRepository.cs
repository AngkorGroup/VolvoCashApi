using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class TransferRepository : Repository<Transfer, MainDbContext>, ITransferRepository
    {
        #region Constructor
        public TransferRepository(MainDbContext dbContext,
                                  ILogger<Repository<Transfer, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
