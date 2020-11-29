using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RefundAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class RefundRepository : Repository<Refund, MainDbContext>, IRefundRepository
    {
        #region Constructor
        public RefundRepository(MainDbContext dbContext, ILogger<Repository<Refund, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<Refund>> GetRefunds(DateTime beginDate, DateTime endDate, RefundStatus refundStatus)
        {
            return await FilterAsync(
                filter: l => l.Date >= beginDate
                    && l.Date <= endDate
                    && l.RefundStatus == refundStatus,
                includeProperties: "TotalAmount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id));
        }

        public async Task<Refund> GetRefund(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "TotalAmount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<Refund> GetRefundWithLiquidationsAndCharges(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "TotalAmount.Currency,BankAccount.Bank,Liquidations.Charges",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }
        #endregion
    }
}
