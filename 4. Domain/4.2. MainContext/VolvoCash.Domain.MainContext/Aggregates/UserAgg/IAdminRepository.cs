using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin> LoginAsync(string email, string passwordHash);
        Task<Admin> GetAdminByEmailAsync(string email);
        Task RemoveRolAdmins(Admin admin);
    }
}
