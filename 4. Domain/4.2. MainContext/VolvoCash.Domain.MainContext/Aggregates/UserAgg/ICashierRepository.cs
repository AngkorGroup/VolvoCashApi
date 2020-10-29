using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public interface ICashierRepository : IRepository<Cashier>
    {
        Task<Cashier> LoginAsync(string email, string passwordHash);
    }
}
