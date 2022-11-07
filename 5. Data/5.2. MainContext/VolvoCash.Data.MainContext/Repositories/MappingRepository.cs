using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class MappingRepository : Repository<Mapping, MainDbContext>, IMappingRepository
    {
        #region Constructor
        public MappingRepository(MainDbContext dbContext, ILogger<Repository<Mapping, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
