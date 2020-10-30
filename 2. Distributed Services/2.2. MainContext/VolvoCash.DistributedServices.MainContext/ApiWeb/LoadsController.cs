using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Loads.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class LoadsController : ControllerBase
    {
        #region Members
        private readonly ILoadAppService _loadAppService;
        #endregion

        #region Constructor
        public LoadsController(ILoadAppService loadAppService)
        {
            _loadAppService = loadAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetLoads()
        {
            var loads = await _loadAppService.GetLoads();
            return Ok(loads);
        }

        [HttpGet("errors")]
        public async Task<ActionResult> GetErrorLoads()
        {
            var loads = await _loadAppService.GetErrorLoads();
            return Ok(loads);
        }

        [HttpPost]
        public async Task<ActionResult> PerformLoad()
        {
            var file = Request.Form.Files["file"];
            var batchErrors = await _loadAppService.PerformLoadsFromFileStreamAsync(file?.FileName, file?.OpenReadStream());
            return Ok(batchErrors);
        }
        #endregion
    }
}
