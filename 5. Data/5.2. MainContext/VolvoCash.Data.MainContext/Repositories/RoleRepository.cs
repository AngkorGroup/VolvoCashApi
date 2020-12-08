using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class RoleRepository : Repository<Role, MainDbContext>, IRoleRepository
    {
        #region Members
        private readonly IRoleMenuRepository _roleMenuRepository;
        #endregion

        #region Constructor
        public RoleRepository(MainDbContext dbContext,
                              ILogger<Repository<Role, MainDbContext>> logger,
                              IRoleMenuRepository roleMenuRepository) : base(dbContext, logger)
        {
            _roleMenuRepository = roleMenuRepository;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            var roles = await FilterAsync(includeProperties: "RoleMenus.Menu.MenuParent");
            foreach (var role in roles)
            {
                role.RoleMenus = role.RoleMenus.OrderBy(rm => rm.Menu.Order).ToList();
            }
            return roles;
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            var role = (await FilterAsync(
                    filter: r => r.Id == id,
                    includeProperties: "RoleMenus.Menu.MenuParent"
                )).FirstOrDefault();

            role.RoleMenus = role.RoleMenus.OrderBy(rm => rm.Menu.Order).ToList();
            return role;
        }

        public async Task RemoveRolMenus(Role role)
        {
            foreach (var roleMenu in role.RoleMenus)
            {
                _roleMenuRepository.Remove(roleMenu);
            }
            await _roleMenuRepository.UnitOfWork.CommitAsync();
        }
        #endregion
    }
}
