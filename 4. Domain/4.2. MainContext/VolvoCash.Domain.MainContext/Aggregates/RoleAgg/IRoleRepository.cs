using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.RoleAgg
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleAsync(int id);
        Task RemoveRolMenus(Role role);
    }
}
