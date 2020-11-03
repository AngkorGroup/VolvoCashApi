using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class DealerRepository : Repository<Dealer, MainDbContext>, IDealerRepository
    {
        #region Constructor
        public DealerRepository(MainDbContext dbContext,
                                ILogger<Repository<Dealer, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
