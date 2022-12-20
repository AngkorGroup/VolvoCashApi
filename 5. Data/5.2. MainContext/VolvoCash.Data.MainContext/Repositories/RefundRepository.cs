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
                filter: l => l.Date.Date >= beginDate
                    && l.Date.Date <= endDate
                    && l.RefundStatus == refundStatus,
                includeProperties: "Amount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id));
        }

        public async Task<Refund> GetRefund(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Amount.Currency,BankAccount.Bank",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<Refund> GetRefundWithLiquidationsAndCharges(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Amount.Currency,BankAccount.Bank,Liquidations.Charges",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public async Task<Refund> GetRefundWithLiquidations(int id)
        {
            return (await FilterAsync(
                filter: l => l.Id == id,
                includeProperties: "Amount.Currency,BankAccount.Bank,Liquidations",
                orderBy: lq => lq.OrderByDescending(l => l.Id))).FirstOrDefault();
        }

        public void SendSap(int id)
        {
            var company = "001";
            var branch = "001";
            var connectedUser = _context._applicationUser.GetName();
            var userName = string.IsNullOrEmpty(connectedUser) ? "Anonymous" : connectedUser;
            userName = userName.Substring(0, Math.Min(userName.Length, 10));
            var command = "TEL01.PKG_REM_MAPPING_CONTABLE_I.P_REM_MAPPING_CONTABLE";
            var parameters = new Dictionary<string, string>()
            {
                { "p_company",  company},
                { "p_branch", branch},
                { "p_user", userName },
                { "p_id", id.ToString() }
            };
            _context.ExecuteCommand(command, parameters);
        }
        #endregion
    }
}
