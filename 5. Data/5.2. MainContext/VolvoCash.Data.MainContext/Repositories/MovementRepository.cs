using VolvoCash.Data.Seedwork;
using Microsoft.Extensions.Logging;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        #region Public Methods
        public async Task<IEnumerable<Movement>> GetMovementsByCard(int cardId)
        {
            return await FilterAsync(filter: m => m.CardId == cardId,
                includeProperties: "Charge.Cashier.Dealer,Transfer", 
                orderBy: c=> c.OrderByDescending(m=>m.Id) );
        }
        #endregion
    }
}
