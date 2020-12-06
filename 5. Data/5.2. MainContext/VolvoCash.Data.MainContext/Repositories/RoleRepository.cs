using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class RoleRepository : Repository<Role, MainDbContext>, IRoleRepository
    {
        #region Constructor
        public RoleRepository(MainDbContext dbContext, ILogger<Repository<Role, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
