using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class ChargeRepository : Repository<Charge, MainDbContext>, IChargeRepository
    {
        #region Constructor
        public ChargeRepository(MainDbContext dbContext,
                                ILogger<Repository<Charge, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion

        public async Task<IEnumerable<Charge>> GetChargesToLiquidate()
        {
            return await FilterAsync(
                filter: c => c.LiquidationId == null
                    && c.Status == ChargeStatus.Accepted
                    && c.OperationDate != null
                    && !c.HasBeenRefunded,
                includeProperties: "Cashier.Dealer,Amount.Currency");
        }

        public async Task<IEnumerable<Charge>> GetChargesByLiquidationId(int liquidationId)
        {
            return await FilterAsync(filter: c => c.LiquidationId == liquidationId, includeProperties: "Cashier.Dealer,Card.CardType,Card.Contact.Client");
        }
    }
}
