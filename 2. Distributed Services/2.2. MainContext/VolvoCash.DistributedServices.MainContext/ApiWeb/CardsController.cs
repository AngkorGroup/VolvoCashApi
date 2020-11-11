using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Cards.Services;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
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
        public async Task<ActionResult> GetCardsByFilter([FromQuery] string query = "", [FromQuery] int maxRecords = 5)
        {
            var cards = await _cardAppService.GetCardsByFilter(query, maxRecords);
            return Ok(cards);
        }

        [HttpGet("by_client")]
        public async Task<ActionResult> GetCardsByClientId([FromQuery] int? clientId = null, [FromQuery] int? contactId = null)
        {
            var cards = await _cardAppService.GetCardsByClientId(clientId, contactId);
            return Ok(cards);
        }

        [HttpGet("by_client_and_card_type")]
        public async Task<ActionResult> GetCardsByClientIdAndCardTypeId([FromQuery] int clientId, [FromQuery] int cardTypeId)
        {
            var cards = await _cardAppService.GetCardsByClientIdAndCardTypeId(clientId, cardTypeId);
            return Ok(cards);
        }

        [HttpGet("{id}/batchs/{batchId}/movements")]
        public async Task<ActionResult> GetCardBatchMovements([FromRoute] int id, [FromRoute] int batchId)
        {
            var cards = await _cardAppService.GetCardBatchMovements(id, batchId);
            return Ok(cards);
        }
        #endregion
    }
}
