using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class CardBatchRepository : Repository<CardBatch, MainDbContext>, ICardBatchRepository
    {
        #region Constructor
        public CardBatchRepository(MainDbContext dbContext,
                                   ILogger<Repository<CardBatch, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
