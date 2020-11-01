using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Cards.Services;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CardsController : ControllerBase
    {
        #region Members
        private readonly ICardAppService _cardAppService;
        #endregion

        #region Constructor
        public CardsController(ICardAppService cardAppService)
        {
            _cardAppService = cardAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet("by_filter")]
        public async Task<ActionResult> GetCardsByFilter([FromQuery] string query = "")
        {
            var cards = await _cardAppService.GetCardsByFilter(query);
            return Ok(cards);
        }

        [HttpGet("by_client")]
        public async Task<ActionResult> GetCardsByClientId([FromQuery] int clientId)
        {
            var cards = await _cardAppService.GetCardsByClientId(clientId);
            return Ok(cards);
        }

        [HttpGet("by_client_and_card_type")]
        public async Task<ActionResult> GetCardsByClientIdAndCardTypeId([FromQuery] int clientId, [FromQuery] int cardTypeId)
        {
            var cards = await _cardAppService.GetCardsByClientIdAndCardTypeId(clientId, cardTypeId);
            return Ok(cards);
        }
        #endregion
    }
}
