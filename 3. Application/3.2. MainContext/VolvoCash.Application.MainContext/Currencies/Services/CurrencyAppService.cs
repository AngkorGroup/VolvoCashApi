using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.EnumAgg;

namespace VolvoCash.Application.MainContext.Currencies.Services
{
    public class CurrencyAppService : Service<Currency, CurrencyDTO>, ICurrencyAppService
    {
        #region Members
        #endregion

        #region Construvtor
        public CurrencyAppService(ICurrencyRepository currencyRepository):base(currencyRepository)
        {
        }
        #endregion

        #region ApiWeb Public Methods

        #endregion
    }
}
