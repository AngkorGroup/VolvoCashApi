using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.Cashiers.Services
{
    public class CashierAppService : Service<Cashier, CashierDTO>, ICashierAppService
    {
        #region Members
        private readonly ICashierRepository _cashierRepository;
        private readonly ILogger _logger;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public CashierAppService(ICashierRepository cashierRepository,
                                 ILogger<CashierAppService> logger) : base(cashierRepository)
        {
            _cashierRepository = cashierRepository;
            _logger = logger;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

    }
}
