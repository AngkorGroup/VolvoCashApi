using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Application.MainContext.MappingHeaders.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/mapping_headers")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MappingHeadersController : ControllerBase
    {
        #region Members
        private readonly IMappingHeaderAppService _mappingHeaderAppService;
        #endregion

        #region Constructor
        public MappingHeadersController(IMappingHeaderAppService mappingHeaderAppService)
        {
            _mappingHeaderAppService = mappingHeaderAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetMappingHeaders([FromQuery] bool onlyActive = false)
        {
            return Ok(await _mappingHeaderAppService.GetMappingHeaders(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMappingHeader([FromRoute] int id)
        {
            return Ok(await _mappingHeaderAppService.GetMappingHeader(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostMappingHeader([FromBody] MappingHeaderDTO mappingHeaderDTO)
        {
            return Ok(await _mappingHeaderAppService.AddAsync(mappingHeaderDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutMappingHeader([FromBody] MappingHeaderDTO mappingHeaderDTO)
        {
            return Ok(await _mappingHeaderAppService.ModifyAsync(mappingHeaderDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMappingHeader([FromRoute] int id)
        {
            await _mappingHeaderAppService.Delete(id);
            return Ok();
        }

        [HttpGet("{id}/mapping_details")]
        public async Task<IActionResult> GetMappingDetails([FromRoute] int id)
        {
            return Ok(await _mappingHeaderAppService.GetMappingDetails(id));
        }
        #endregion
    }
}
