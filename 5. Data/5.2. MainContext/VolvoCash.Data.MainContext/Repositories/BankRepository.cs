using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class BankRepository : Repository<Bank, MainDbContext>, IBankRepository
    {
        #region Constructor
        public BankRepository(MainDbContext dbContext, ILogger<Repository<Bank, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
