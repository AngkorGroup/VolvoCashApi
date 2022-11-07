using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class MappingDetailRepository : Repository<MappingDetail, MainDbContext>, IMappingDetailRepository
    {
        #region Constructor
        public MappingDetailRepository(MainDbContext dbContext, ILogger<Repository<MappingDetail, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
