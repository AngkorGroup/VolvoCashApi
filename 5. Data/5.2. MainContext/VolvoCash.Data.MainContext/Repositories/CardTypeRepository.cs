using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class CardTypeRepository : Repository<CardType, MainDbContext>, ICardTypeRepository
    {
        #region Constructor
        public CardTypeRepository(MainDbContext dbContext,
                                  ILogger<Repository<CardType, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
