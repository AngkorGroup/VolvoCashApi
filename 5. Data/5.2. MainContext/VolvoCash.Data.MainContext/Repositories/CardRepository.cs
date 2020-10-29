using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class CardRepository : Repository<Card, MainDbContext>, ICardRepository
    {
        #region Constructor
        public CardRepository(MainDbContext dbContext,
                              ILogger<Repository<Card, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
