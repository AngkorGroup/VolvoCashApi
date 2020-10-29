using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Cards.Services;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [Authorize(Roles = "Contact")]
    [ApiController]
    [Route("api_client/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CardsController : ControllerBase
    {
        #region Members
        private readonly ICardAppService _cardAppService;
        private readonly IUrlManager _urlManager;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public CardsController(ICardAppService cardAppService,
                               IUrlManager urlManager, 
                               IApplicationUser applicationUser)
        {
            _cardAppService = cardAppService;
            _urlManager = urlManager;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetCards()
        {
            var cards = await _cardAppService.GetCardsByPhone(_applicationUser.GetUserName());
            return Ok(cards);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetCard([FromRoute] int id)
        {
            var card = await _cardAppService.GetCardByPhone(_applicationUser.GetUserName(), id);
             card.QrUrl = _urlManager.GetQrUrl(card.GetCardForQr());
            return Ok(card);
        }
        #endregion
    }
}
