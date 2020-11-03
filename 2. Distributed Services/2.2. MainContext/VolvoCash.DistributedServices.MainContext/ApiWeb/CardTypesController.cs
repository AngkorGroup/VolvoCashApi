using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.CardTypes.Services;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.DistributedServices.Seedwork.Controllers;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CardTypesController : AsyncBaseApiController<CardType, CardTypeDTO>
    {
        #region Members
        private readonly ICardTypeAppService _cardTypeAppService;
        #endregion

        #region Constructor
        public CardTypesController(ICardTypeAppService cardTypeAppService) : base(cardTypeAppService)
        {
            _cardTypeAppService = cardTypeAppService;
        }
        #endregion
    }
}
