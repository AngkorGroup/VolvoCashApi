using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.CardTypes.Services;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CardTypesController : ControllerBase
    {
        private readonly ICardTypeAppService _cardTypeAppService;

        #region Constructor
        public CardTypesController(ICardTypeAppService cardTypeAppService) 
        {
            _cardTypeAppService = cardTypeAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetCardTypes([FromQuery] bool onlyActive = false)
        {
            return Ok(await _cardTypeAppService.GetCardTypes(onlyActive));
        }

        [HttpPost]
        public async Task<IActionResult> PostCardType([FromBody] CardTypeDTO cardTypeDTO)
        {
            return Ok(await _cardTypeAppService.AddAsync(cardTypeDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutCardType([FromBody] CardTypeDTO cardTypeDTO)
        {
            return Ok(await _cardTypeAppService.ModifyAsync(cardTypeDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardType([FromRoute] int id)
        {
            await _cardTypeAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
