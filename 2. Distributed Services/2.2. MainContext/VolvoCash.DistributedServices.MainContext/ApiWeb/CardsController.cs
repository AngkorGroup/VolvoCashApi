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
        #endregion
    }
}
