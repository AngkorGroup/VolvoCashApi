using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Liquidations.Services;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class LiquidationsController : ControllerBase
    {
        #region Members
        private readonly ILiquidationAppService _liquidationAppService;
        #endregion

        #region Constructor
        public LiquidationsController(ILiquidationAppService liquidationAppService) 
        {
            _liquidationAppService = liquidationAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetLiquidations([FromQuery] string date, [FromQuery] int statusId)
        {
            var _date = DateTimeParser.ParseString(date, DateTimeFormats.DateFormat);
            return Ok(await _liquidationAppService.GetLiquidations(_date, (LiquidationStatus)statusId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLiquidation([FromRoute] int id)
        {
            return Ok(await _liquidationAppService.GetLiquidation(id));
        }

        [HttpGet("{id}/charges")]
        public async Task<IActionResult> GetLiquidationCharges([FromRoute] int id)
        {
            return Ok(await _liquidationAppService.GetLiquidationCharges(id));
        }

        [HttpGet("generate")]
        public async Task<IActionResult> PostLiquidation()
        {
            await _liquidationAppService.GenerateLiquidations();
            return Ok();
        }

        [HttpPost("{id}/program")]
        public async Task<IActionResult> ProgramLiquidation([FromRoute] int id)
        {
            //await _liquidationAppService.AnulateLiquidation(id);
            return Ok();
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PayLiquidation([FromRoute] int id)
        {
            //await _liquidationAppService.AnulateLiquidation(id);
            return Ok();
        }

        [HttpPost("{id}/anulate")]
        public async Task<IActionResult> AnulateLiquidation([FromRoute] int id)
        {
            //await _liquidationAppService.AnulateLiquidation(id);
            return Ok();
        }
        #endregion
    }
}
