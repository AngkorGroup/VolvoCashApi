using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.SectorAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class SectorRepository : Repository<Sector, MainDbContext>, ISectorRepository
    {
        #region Constructor
        public SectorRepository(MainDbContext dbContext, ILogger<Repository<Sector, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
