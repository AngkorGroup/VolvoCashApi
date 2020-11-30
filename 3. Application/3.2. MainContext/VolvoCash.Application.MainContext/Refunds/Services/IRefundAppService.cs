using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Refunds;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Refunds.Services
{
    public interface IRefundAppService : IDisposable
    {
        #region ApiWeb
        Task<List<RefundDTO>> GetRefunds(DateTime beginDate, DateTime endDate, RefundStatus refundStatus);
        Task<RefundDTO> GetRefund(int id);
        Task PayRefund(int id, string voucher, DateTime paymentDate);
        Task<List<LiquidationListDTO>> GetRefundLiquidations(int id);
        #endregion
    }
}
