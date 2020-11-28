using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Cashiers.Services;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CashiersController : ControllerBase
    {
        #region Members
        private readonly ICashierAppService _cashierAppService;
        #endregion

        #region Constructor
        public CashiersController(ICashierAppService cashierAppService)
        {
            _cashierAppService = cashierAppService;
        }
        #endregion

        #region Public Methods   
        [HttpGet]
        public async Task<IActionResult> GetCashiers([FromQuery] bool onlyActive = false)
        {
            return Ok(await _cashierAppService.GetCashiers(onlyActive));
        }

        [HttpPost]
        public async Task<IActionResult> PostCashier([FromBody] CashierDTO cashierDTO)
        {
            return Ok(await _cashierAppService.AddAsync(cashierDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutCashier([FromBody] CashierDTO cashierDTO)
        {
            return Ok(await _cashierAppService.ModifyAsync(cashierDTO));
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _cashierAppService.Delete(id);
        }
        #endregion
    }
}
