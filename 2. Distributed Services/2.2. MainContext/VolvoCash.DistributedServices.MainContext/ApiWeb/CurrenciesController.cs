using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Currencies.Services;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CurrenciesController : ControllerBase
    {
        #region Members
        private readonly ICurrencyAppService _currencyAppService;
        #endregion

        #region Constructor
        public CurrenciesController(ICurrencyAppService currencyAppService) 
        {
            _currencyAppService = currencyAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetCurrencies([FromQuery] bool onlyActive = false)
        {
            return Ok(await _currencyAppService.GetCurrencies(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrency([FromRoute] int id)
        {
            return Ok(await _currencyAppService.GetCurrency(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostCurrency([FromBody] CurrencyDTO currencyDTO)
        {
            return Ok(await _currencyAppService.AddAsync(currencyDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutCurrency([FromBody] CurrencyDTO currencyDTO)
        {
            return Ok(await _currencyAppService.ModifyAsync(currencyDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency([FromRoute] int id)
        {
            await _currencyAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
