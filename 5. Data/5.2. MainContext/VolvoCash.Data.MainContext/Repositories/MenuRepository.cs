using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MenuAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class MenuRepository : Repository<Menu, MainDbContext>, IMenuRepository
    {
        #region Constructor
        public MenuRepository(MainDbContext dbContext, ILogger<Repository<Menu, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
