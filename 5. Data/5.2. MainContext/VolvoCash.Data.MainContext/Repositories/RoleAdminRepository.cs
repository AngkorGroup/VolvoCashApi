using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class RoleAdminRepository : Repository<RoleAdmin, MainDbContext>, IRoleAdminRepository
    {
        #region Constructor
        public RoleAdminRepository(MainDbContext dbContext, ILogger<Repository<RoleAdmin, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
