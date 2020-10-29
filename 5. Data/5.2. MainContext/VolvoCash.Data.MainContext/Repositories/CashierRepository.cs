using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class CashierRepository : Repository<Cashier, MainDbContext>, ICashierRepository
    {
        #region Constructor
        public CashierRepository(MainDbContext dbContext,
                                 ILogger<Repository<Cashier, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion

        #region Public Methods
        public async Task<Cashier> LoginAsync(string email, string passwordHash)
        {
            var cashier = await _context.Cashiers.FirstOrDefaultAsync(c => c.Email == email && c.PasswordHash == passwordHash);
            return cashier;
        }
        #endregion
    }
}
