using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Clients.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ClientsController : ControllerBase
    {
        #region Members
        private readonly IClientAppService _clientAppService;
        #endregion

        #region Constructor
        public ClientsController(IClientAppService clientAppService)
        {
            _clientAppService = clientAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet("by_filter")]
        public async Task<ActionResult> GetClientsByFilter([FromQuery] string query = "", [FromQuery] int maxRecords = 5)
        {
            var clients = await _clientAppService.GetClientsByFilter(query, maxRecords);
            return Ok(clients);
        }

        [HttpGet("by_pagination")]
        public async Task<ActionResult> GetClientsByPagination([FromQuery] string query = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageLength = 10)
        {
            var clients = await _clientAppService.GetClientsByPagination(query, pageIndex, pageLength);
            return Ok(clients);
        }

        [HttpGet("{id}/card_types_summary")]
        public async Task<ActionResult> GetClientCardTypesSummary([FromRoute] int id)
        {
            var cardTypesSummary = await _clientAppService.GetClientCardTypesSummary(id);
            return Ok(cardTypesSummary);
        }
        #endregion
    }
}
