using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

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
            _roleAdminRepository = roleAdminRepository;
        }
        #endregion

        #region Public Methods
        public async Task<Admin> LoginAsync(string email, string passwordHash)
        {
            var admin = (await FilterAsync(
                    filter: a => a.Email.ToUpper().Trim() == email.ToUpper().Trim() 
                    && a.PasswordHash == passwordHash 
                    && a.Status == Status.Active,
                    includeProperties: "Dealer,RoleAdmins.Role.RoleMenus.Menu.MenuParent")
                ).FirstOrDefault();
            admin.SetMenuOptions();
            return admin;
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            var admin = (await FilterAsync(
                    filter: a => a.Email.ToUpper().Trim() == email.ToUpper().Trim() && a.Status == Status.Active)
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
