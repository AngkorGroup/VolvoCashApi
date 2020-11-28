using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class CurrencyRepository : Repository<Currency, MainDbContext>, ICurrencyRepository
    {
        #region Constructor
        public CurrencyRepository(MainDbContext dbContext, ILogger<Repository<Currency, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
