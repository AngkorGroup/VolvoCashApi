using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.CardTypes.Services
{
    public class CardTypeAppService : Service<CardType, CardTypeDTO>, ICardTypeAppService
    {
        #region Members
        private readonly ICardTypeRepository _cardTypeRepository;
        private readonly ILogger _logger;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public CardTypeAppService(ICardTypeRepository cardTypeRepository,
                                  ILogger<CardTypeAppService> logger)
        {
            _cardTypeRepository = cardTypeRepository;
            _logger = logger;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

    }
}
