using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.CrossCutting.Localization;

namespace VolvoCash.Application.MainContext.Dealers.Services
{
    public class DealerAppService : Service<Dealer, DealerDTO>, IDealerAppService
    {
        #region Members
        private readonly IDealerRepository _dealerRepository;
        private readonly ILogger _logger;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public DealerAppService(IDealerRepository dealerRepository,
                                ILogger<DealerAppService> logger) : base(dealerRepository)
        {
            _dealerRepository = dealerRepository;
            _logger = logger;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

    }
}
