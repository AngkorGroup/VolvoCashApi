using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountTypeAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class BankAccountTypeRepository : Repository<BankAccountType, MainDbContext>, IBankAccountTypeRepository
    {
        #region Constructor
        public BankAccountTypeRepository(MainDbContext dbContext, ILogger<Repository<BankAccountType, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
