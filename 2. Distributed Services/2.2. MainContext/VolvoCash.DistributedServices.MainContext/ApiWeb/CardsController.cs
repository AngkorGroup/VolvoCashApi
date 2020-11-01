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
        [HttpGet]
        public async Task<ActionResult> GetCards([FromQuery] string query = "")
        {
            var cards = await _cardAppService.GetCards(query);
            return Ok(cards);
        }
        #endregion
    }
}
