using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class BankAccountRepository : Repository<BankAccount, MainDbContext>, IBankAccountRepository
    {
        #region Constructor
        public BankAccountRepository(MainDbContext dbContext, ILogger<Repository<BankAccount, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
