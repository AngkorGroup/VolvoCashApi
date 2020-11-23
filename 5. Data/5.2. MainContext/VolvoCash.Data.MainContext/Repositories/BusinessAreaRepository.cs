using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class BusinessAreaRepository : Repository<BusinessArea, MainDbContext>, IBusinessAreaRepository
    {
        #region Constructor
        public BusinessAreaRepository(MainDbContext dbContext, ILogger<Repository<BusinessArea, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
