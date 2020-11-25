using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class LiquidationRepository : Repository<Liquidation, MainDbContext>, ILiquidationRepository
    {
        #region Constructor
        public LiquidationRepository(MainDbContext dbContext, ILogger<Repository<Liquidation, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<Liquidation>> GetLiquidations(DateTime date, LiquidationStatus liquidationStatus)
        {
            return await FilterAsync(
                filter: l => l.Date == date && l.LiquidationStatus == liquidationStatus,
                includeProperties: "Dealer,Amount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id));
        }

        public async Task<Liquidation> GetLiquidation(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Dealer,Amount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<Liquidation> GetLiquidationWithCharges(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Dealer,Amount.Currency,BankAccount.Bank,Charges",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<IEnumerable<Charge>> GetLiquidationChargesAsync(int id)
        {
            return (await FilterAsync(filter: l => l.Id == id, includeProperties: "Charges")).FirstOrDefault().Charges;
        }        
        #endregion
    }
}
