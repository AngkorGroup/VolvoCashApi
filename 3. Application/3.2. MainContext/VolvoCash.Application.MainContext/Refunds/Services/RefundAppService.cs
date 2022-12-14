using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Application.MainContext.DTO.Refunds;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.Domain.MainContext.Aggregates.RefundAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Refunds.Services
{
    public class RefundAppService : IRefundAppService
    {
        #region Members
        private readonly IRefundRepository _refundRepository;
        private readonly ILiquidationRepository _liquidationRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public RefundAppService(IRefundRepository refundRepository,
                                ILiquidationRepository liquidationRepository)
        {
            _refundRepository = refundRepository;
            _liquidationRepository = liquidationRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<RefundDTO>> GetRefunds(DateTime beginDate, DateTime endDate, RefundStatus refundStatus)
        {
            var refunds = await _refundRepository.GetRefunds(beginDate, endDate, refundStatus);
            return refunds.ProjectedAsCollection<RefundDTO>();
        }

        public async Task<RefundDTO> GetRefund(int id)
        {
            var refund = await _refundRepository.GetAsync(id);
            return refund.ProjectedAs<RefundDTO>();
        }

        public async Task<List<LiquidationListDTO>> GetRefundLiquidations(int id)
        {
            var refunds = await _liquidationRepository.GetLiquidationsByRefundId(id);
            return refunds.ProjectedAsCollection<LiquidationListDTO>();
        }

        public async Task PayRefund(int id, string voucher, DateTime paymentDate)
        {
            var refund = await _refundRepository.GetRefundWithLiquidationsAndCharges(id);
            if (refund.RefundStatus != RefundStatus.Scheduled)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidRefundStatusForPay));
            }
            refund.PayRefund(voucher, paymentDate);
            await _refundRepository.UnitOfWork.CommitAsync();
        }

        public async Task CancelRefund(int id)
        {
            var refund = await _refundRepository.GetRefundWithLiquidationsAndCharges(id);
            if (refund.RefundStatus != RefundStatus.Scheduled)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidRefundStatusForCancel));
            }
            refund.CancelRefund();
            await _refundRepository.UnitOfWork.CommitAsync();
        }

        public void SendSap(int id)
        {
            _refundRepository.SendSap(id);
        }

        public async Task ResendSap(int id)
        {
            var refund = await _refundRepository.GetRefundWithLiquidationsAndCharges(id);
            refund.RollbackRefundFromSap();
            await _refundRepository.UnitOfWork.CommitAsync();
            _refundRepository.SendSap(id);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _liquidationRepository.Dispose();
        }
        #endregion
    }
}
