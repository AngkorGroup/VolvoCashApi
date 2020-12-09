using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Liquidations.Services;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.CrossCutting.Utils.Constants;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Liquidations;
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
        public async Task<IActionResult> GetLiquidations([FromQuery] string beginDate,
                                                         [FromQuery] string endDate, [FromQuery] string status)
        {
            var _beginDate = DateTimeParser.ParseString(beginDate, DateTimeFormats.DateFormat);
            var _endDate = DateTimeParser.ParseString(endDate, DateTimeFormats.DateFormat);
            var liquidationStatus = (LiquidationStatus)EnumParser.ToEnum<LiquidationStatus>(status);
            var liquidations = await _liquidationAppService.GetLiquidations(_beginDate, _endDate, liquidationStatus);
            return Ok(liquidations);
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
        public async Task<IActionResult> GenerateLiquidations()
        {
            await _liquidationAppService.GenerateLiquidations();
            return Ok();
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleLiquidations([FromBody] ScheduleLiquidationsRequest request)
        {
            byte[] content = await _liquidationAppService.ScheduleLiquidations(request.BankId, request.BankAccountId, request.LiquidationsId);
            return File(content, "text/plain", "ArchivoBanco.txt");
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PayLiquidation([FromRoute] int id, [FromBody] PayLiquidationRequest request)
        {
            var paymentDate = DateTimeParser.ParseString(request.PaymentDate, DateTimeFormats.DateFormat);
            await _liquidationAppService.PayLiquidation(id, request.Voucher, paymentDate);
            return Ok();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelLiquidation([FromRoute] int id)
        {
            await _liquidationAppService.CancelLiquidation(id);
            return Ok();
        }
        #endregion
    }
}
