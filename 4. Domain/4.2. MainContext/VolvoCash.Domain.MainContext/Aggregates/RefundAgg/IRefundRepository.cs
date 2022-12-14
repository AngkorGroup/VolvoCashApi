using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.RefundAgg
{
    public interface IRefundRepository : IRepository<Refund>
    {
        Task<IEnumerable<Refund>> GetRefunds(DateTime beginDate, DateTime endDate, RefundStatus refundStatus);
        Task<Refund> GetRefund(int id);
        Task<Refund> GetRefundWithLiquidationsAndCharges(int id);
        Task<Refund> GetRefundWithLiquidations(int id);
        void SendSap(int id);
    }
}
