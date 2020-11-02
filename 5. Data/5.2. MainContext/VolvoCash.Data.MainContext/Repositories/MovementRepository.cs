using VolvoCash.Data.Seedwork;
using Microsoft.Extensions.Logging;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class MovementRepository : Repository<Movement, MainDbContext>, IMovementRepository
    {
        #region Constructor
        public MovementRepository(MainDbContext dbContext,
                                  ILogger<Repository<Movement, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
