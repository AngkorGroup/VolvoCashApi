using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class UserRepository : Repository<User, MainDbContext>, IUserRepository
    {
        #region Constructor
        public UserRepository(MainDbContext dbContext,
                              ILogger<Repository<User, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
