using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
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
        public async Task<IEnumerable<Liquidation>> GetLiquidations(DateTime beginDate, DateTime endDate, LiquidationStatus liquidationStatus)
        {
            return await FilterAsync(
                filter: l => l.Date >= beginDate
                           && l.Date <= endDate
                           && l.LiquidationStatus == liquidationStatus,
                includeProperties: "Dealer,Amount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Date));
        }

        public async Task<Liquidation> GetLiquidation(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Dealer,Amount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<Liquidation> GetLiquidationForScheduled(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Dealer.BankAccounts.BankAccountType.BankBankAccountTypes,Amount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<Liquidation> GetLiquidationWithCharges(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Dealer,Amount.Currency,BankAccount.Bank,Charges",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<IEnumerable<Liquidation>> GetLiquidationsByRefundId(int refundId)
        {
            return await FilterAsync(filter: c => c.RefundId == refundId, includeProperties: "Dealer,Amount.Currency,BankAccount.Bank,Charges");
        }
        #endregion
    }
}
