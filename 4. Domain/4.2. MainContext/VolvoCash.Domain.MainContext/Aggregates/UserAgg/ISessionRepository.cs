using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<List<string>> GetActivePushDeviceTokensAsync(int userId);
    }
}
