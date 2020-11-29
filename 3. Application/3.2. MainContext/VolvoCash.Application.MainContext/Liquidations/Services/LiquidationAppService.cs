using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.Domain.MainContext.Aggregates.RefundAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Services.BankService;

namespace VolvoCash.Application.MainContext.Liquidations.Services
{
    public class LiquidationAppService : ILiquidationAppService
    {
        #region Members
        private readonly ILiquidationService _liquidationService;
        private readonly ILiquidationRepository _liquidationRepository;
        private readonly IBankLiquidationService _bankLiquidationService;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IChargeRepository _chargeRepository;
        private readonly IRefundRepository _refundRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public LiquidationAppService(ILiquidationRepository liquidationRepository,
                                     ILiquidationService liquidationService,
                                     IBankLiquidationService bankLiquidationService,
                                     IBankAccountRepository bankAccountRepository,
                                     IChargeRepository chargeRepository,
                                     IRefundRepository refundRepository)
        {
            _liquidationService = liquidationService;
            _liquidationRepository = liquidationRepository;
            _bankLiquidationService = bankLiquidationService;
            _bankAccountRepository = bankAccountRepository;
            _chargeRepository = chargeRepository;
            _refundRepository = refundRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<LiquidationDTO>> GetLiquidations(DateTime beginDate, DateTime endDate, LiquidationStatus liquidationStatus)
        {
            var liquidations = await _liquidationRepository.GetLiquidations(beginDate, endDate, liquidationStatus);
            return liquidations.ProjectedAsCollection<LiquidationDTO>();
        }

        public async Task<LiquidationDTO> GetLiquidation(int id)
        {
            var liquidation = await _liquidationRepository.GetAsync(id);
            return liquidation.ProjectedAs<LiquidationDTO>();
        }

        public async Task<List<ChargeListDTO>> GetLiquidationCharges(int id)
        {
            var charges = await _chargeRepository.GetChargesByLiquidationId(id);
            return charges.ProjectedAsCollection<ChargeListDTO>();
        }

        public async Task<List<LiquidationDTO>> GenerateLiquidations()
        {
            var liquidations = await _liquidationService.GenerateLiquidationsAsync();
            _liquidationRepository.Add(liquidations);
            await _liquidationRepository.UnitOfWork.CommitAsync();

            return liquidations.ProjectedAsCollection<LiquidationDTO>();
        }

        public async Task<byte[]> ScheduleLiquidations(int bankId, int bankAccountId, List<int> liquidationsId)
        {
            var originBankAccount = _bankAccountRepository.Filter(ba => ba.BankId == bankId
                                                                  && ba.Id == bankAccountId
                                                                  && ba.DealerId == null,
                                                                  includeProperties: "BankAccountType.BankBankAccountTypes,Bank,Currency.BankCurrencies")
                                                          .FirstOrDefault();

            if (originBankAccount == null)
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_BankAccountNotFound));

            var liquidations = new List<Liquidation>();
            foreach (var liquidationId in liquidationsId)
            {
                var liquidation = await _liquidationRepository.GetLiquidationForScheduled(liquidationId);

                if (liquidation == null)
                    throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_LiquidationNotFound));

                if (liquidation.LiquidationStatus != LiquidationStatus.Generated && liquidation.LiquidationStatus != LiquidationStatus.Scheduled)
                {
                    throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidLiquidationStatusForSchedule));
                }

                liquidations.Add(liquidation);
            }

            var bankFile = _bankLiquidationService.GenerateBankFile(originBankAccount, liquidations);
            var totalAmount = new Money(originBankAccount.Currency, 0.0);
            foreach (var liquidation in liquidations)
            {
                liquidation.ScheduleLiquidation(originBankAccount);
                totalAmount.Value += liquidation.Amount.Value;
            }
            var refund = new Refund(DateTime.Now, totalAmount, originBankAccount, liquidations);

            await _liquidationRepository.UnitOfWork.CommitAsync();
            await _refundRepository.UnitOfWork.CommitAsync();

            return bankFile;
        }

        public async Task PayLiquidation(int id, string voucher, DateTime paymentDate)
        {
            var liquidation = await _liquidationRepository.GetLiquidationWithCharges(id);
            if (liquidation.LiquidationStatus != LiquidationStatus.Scheduled)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidLiquidationStatusForPay));
            }
            liquidation.PayLiquidation(voucher, paymentDate);
            await _liquidationRepository.UnitOfWork.CommitAsync();
        }

        public async Task CancelLiquidation(int id)
        {
            var liquidation = await _liquidationRepository.GetLiquidationWithCharges(id);
            if (liquidation.LiquidationStatus != LiquidationStatus.Generated)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidLiquidationStatusForCancel));
            }
            liquidation.CancelLiquidation();
            await _liquidationRepository.UnitOfWork.CommitAsync();
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
