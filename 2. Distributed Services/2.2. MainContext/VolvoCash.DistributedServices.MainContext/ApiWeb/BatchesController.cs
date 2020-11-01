using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Batches.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
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
        public async Task<ActionResult> GetBatches()
        {
            var loads = await _batchAppService.GetBatches();
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

        [HttpPost("massive_load")]
        public async Task<ActionResult> PerformBatch()
        {
            var file = Request.Form.Files["file"];
            var batchErrors = await _batchAppService.PerformLoadsFromFileStreamAsync(file?.FileName, file?.OpenReadStream());
            return Ok(batchErrors);
        }
        #endregion
    }
}
