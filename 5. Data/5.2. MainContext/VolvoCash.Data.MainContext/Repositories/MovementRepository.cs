using VolvoCash.Data.Seedwork;
using Microsoft.Extensions.Logging;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using VolvoCash.Domain.MainContext.Enums;

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
            var movements = await FilterAsync(
                filter: m => m.CardId == cardId,
                includeProperties: "Charge.Cashier.Dealer,Transfer,BatchMovements.Batch",
                orderBy: c => c.OrderByDescending(m => m.Id)
            );

            foreach (var movement in movements)
            {
                if (movement.Type == MovementType.REC)
                {
                    var batchMovement = movement.BatchMovements.FirstOrDefault();
                    movement.BatchId = batchMovement.BatchId;
                }
                movement.BatchMovements = null;
            }
            return movements;
        }
        #endregion
    }
}
