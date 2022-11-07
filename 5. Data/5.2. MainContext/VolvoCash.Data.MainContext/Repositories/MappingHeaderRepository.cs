using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class MappingHeaderRepository : Repository<MappingHeader, MainDbContext>, IMappingHeaderRepository
    {
        #region Constructor
        public MappingHeaderRepository(MainDbContext dbContext, ILogger<Repository<MappingHeader, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
