using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class AdminRepository : Repository<Admin, MainDbContext>, IAdminRepository
    {
        #region Constructor
        public AdminRepository(MainDbContext dbContext,
                               ILogger<Repository<Admin, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion

        #region Public Methods
        public async Task<Admin> LoginAsync(string email, string passwordHash)
        {
            var admin = (await FilterAsync(filter: c => c.Email == email
            && c.PasswordHash == passwordHash, includeProperties:"Dealer")).FirstOrDefault();
            return admin;
        }
        #endregion
    }
}
