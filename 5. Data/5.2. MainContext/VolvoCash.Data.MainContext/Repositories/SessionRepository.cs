using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class SessionRepository : Repository<Session, MainDbContext>, ISessionRepository
    {
        #region Members
        #endregion

        #region Constructor
        public SessionRepository(MainDbContext dbContext, ILogger<Repository<Session, MainDbContext>> logger) : base(dbContext, logger)
        {
        }

        public async Task<List<string>> GetActivePushDeviceTokensAsync(int userId)
        {
            var activeSessionsWithDeviceToken = await FilterAsync(s => s.UserId == userId && s.Status == Status.Active && !string.IsNullOrEmpty(s.PushDeviceToken));
            return activeSessionsWithDeviceToken.GroupBy(c => c.PushDeviceToken).Select(t => t.Key).ToList();
        }
        #endregion
    }
}
