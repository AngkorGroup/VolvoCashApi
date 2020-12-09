using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Batches.Services;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.CrossCutting.Utils.Constants;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Batches;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BatchesController : ControllerBase
    {
        #region Members
        private readonly IBatchAppService _batchAppService;
        #endregion

        #region Constructor
        public BatchesController(IBatchAppService batchAppService)
        {
            _batchAppService = batchAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetBatches([FromQuery] string beginDate = "", [FromQuery] string endDate = "")
        {
            var _beginDate = DateTimeParser.TryParseString(beginDate, DateTimeFormats.DateFormat);
            var _endDate = DateTimeParser.TryParseString(endDate, DateTimeFormats.DateFormat);
            var loads = await _batchAppService.GetBatches(_beginDate, _endDate);
            return Ok(loads);
        }

        [HttpGet("expires_at_extent")]
        public async Task<ActionResult> GetBatchesByExpiresAtExtent([FromQuery] string clientId = "all", [FromQuery] string beginDate = "", [FromQuery] string endDate = "")
        {
            var _beginDate = DateTimeParser.TryParseString(beginDate, DateTimeFormats.DateFormat);
            var _endDate = DateTimeParser.TryParseString(endDate, DateTimeFormats.DateFormat);
            var loads = await _batchAppService.GetBatchesByExpiresAtExtent(clientId, _beginDate, _endDate);
            return Ok(loads);
        }

        [HttpGet("by_card")]
        public async Task<ActionResult> GetBatchesByCardId(int cardId)
        {
            var batches = await _batchAppService.GetBatchesByCardId(cardId);
            return Ok(batches);
        }

        [HttpGet("by_client")]
        public async Task<ActionResult> GetBatchesByClientId(int clientId)
        {
            var batches = await _batchAppService.GetBatchesByClientId(clientId);
            return Ok(batches);
        }

        [HttpGet("errors")]
        public async Task<ActionResult> GetErrorBatches()
        {
            var loads = await _batchAppService.GetErrorBatches();
            return Ok(loads);
        }

        [HttpPost("pre_massive_load")]
        public ActionResult PrePerformMassiveLoad()
        {
            var file = Request.Form.Files["file"];
            var batches = _batchAppService.GetLoadsFromFileStream(file?.FileName, file?.OpenReadStream());
            return Ok(batches);
        }

        [HttpPost("massive_load")]
        public async Task<ActionResult> PerformMassiveLoad()
        {
            var file = Request.Form.Files["file"];
            var batchErrors = await _batchAppService.PerformLoadsFromFileStreamAsync(file?.FileName, file?.OpenReadStream());
            return Ok(batchErrors);
        }

        [HttpPost("{id}/extend_expired_date")]
        public async Task<ActionResult> ExtendExpiredDate([FromRoute] int id, [FromBody] ExtendExpiredDateRequest request )
        {
            var newExpiredDate = DateTimeParser.ParseString(request.NewExpiredDate, DateTimeFormats.DateFormat);
            await _batchAppService.ExtendExpiredDate(id, newExpiredDate);
            return Ok();
        }
        #endregion
    }
}
