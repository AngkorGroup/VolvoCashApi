using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class AdminRepository : Repository<Admin, MainDbContext>, IAdminRepository
    {
        #region Members
        private readonly IRoleAdminRepository _roleAdminRepository;
        #endregion

        #region Constructor
        public AdminRepository(MainDbContext dbContext,
                               ILogger<Repository<Admin, MainDbContext>> logger,
                              IRoleAdminRepository roleAdminRepository) : base(dbContext, logger)
        {
        }
        #endregion

        #region Public Methods
        public async Task<Admin> LoginAsync(string email, string passwordHash)
        {
            var admin = (await FilterAsync(
                    filter: c => c.Email == email && c.PasswordHash == passwordHash,
                    includeProperties: "Dealer")
                ).FirstOrDefault();
            return admin;
        }

        public async Task RemoveRolAdmins(Admin admin)
        {
            foreach (var roleAdmin in admin.RoleAdmins)
            {
                _roleAdminRepository.Remove(roleAdmin);
            }
            await _roleAdminRepository.UnitOfWork.CommitAsync();
        }
        #endregion
    }
}
