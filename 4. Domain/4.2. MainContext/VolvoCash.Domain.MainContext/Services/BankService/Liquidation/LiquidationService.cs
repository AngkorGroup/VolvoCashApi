using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;

namespace VolvoCash.Domain.MainContext.Services.BankService
{
    public class LiquidationService : ILiquidationService
    {
        #region Members
        private readonly IChargeRepository _chargeRepository;
        #endregion

        #region Constructor
        public LiquidationService(IChargeRepository chargeRepository)
        {
            _chargeRepository = chargeRepository;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public async Task<IEnumerable<Liquidation>> GenerateLiquidationsAsync()
        {
            var charges = await _chargeRepository.GetChargesToLiquidate();
            var liquidationsToGenerate = new List<Liquidation>();
            foreach (var charge in charges)
            {
                var liquidation = liquidationsToGenerate.Where(l => l.Date == charge.CreatedAt.Date
                                                                    && l.DealerId == charge.Cashier.DealerId
                                                                    && l.Amount.CurrencyId == charge.Amount.CurrencyId).FirstOrDefault();
                if (liquidation == null)
                {
                    liquidation = new Liquidation(charge.CreatedAt.Date, charge.Amount, charge.Cashier.DealerId);
                    liquidationsToGenerate.Add(liquidation);
                }
                else
                {
                    liquidation.AddAmount(charge.Amount);
                }
                liquidation.AddCharge(charge);
            }
            return liquidationsToGenerate;
        }
        #endregion
    }
}
