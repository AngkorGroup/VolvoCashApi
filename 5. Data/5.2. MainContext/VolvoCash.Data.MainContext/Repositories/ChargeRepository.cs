using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class ChargeRepository : Repository<Charge, MainDbContext>, IChargeRepository
    {
        #region Constructor
        public ChargeRepository(MainDbContext dbContext,
                                ILogger<Repository<Charge, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
