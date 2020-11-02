using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Movements.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MovementsController : ControllerBase
    {
        #region Members
        private readonly IMovementAppService _movementAppService;
        #endregion

        #region Constructor
        public MovementsController(IMovementAppService movementAppService)
        {
            _movementAppService = movementAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet("by_card")]
        public async Task<ActionResult> GetMovementsByCardId(int cardId)
        {
            var movements = await _movementAppService.GetMovementsByCardId(cardId);
            return Ok(movements);
        }
        #endregion
    }
}
