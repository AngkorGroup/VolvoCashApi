using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            var admin = await _context.Admins.FirstOrDefaultAsync(c => c.Email == email && c.PasswordHash == passwordHash);
            return admin;
        }
        #endregion
    }
}
