using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class RoleMenuRepository : Repository<RoleMenu, MainDbContext>, IRoleMenuRepository
    {
        #region Constructor
        public RoleMenuRepository(MainDbContext dbContext, ILogger<Repository<RoleMenu, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
