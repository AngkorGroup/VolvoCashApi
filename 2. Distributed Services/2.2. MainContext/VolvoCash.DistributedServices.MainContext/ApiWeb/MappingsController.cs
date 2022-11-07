using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Application.MainContext.Mappings.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MappingsController : ControllerBase
    {
        #region Members
        private readonly IMappingAppService _mappingAppService;
        #endregion

        #region Constructor
        public MappingsController(IMappingAppService mappingAppService)
        {
            _mappingAppService = mappingAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetMappings([FromQuery] bool onlyActive = false)
        {
            return Ok(await _mappingAppService.GetMappings(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMapping([FromRoute] int id)
        {
            return Ok(await _mappingAppService.GetMapping(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostMapping([FromBody] MappingDTO mappingDTO)
        {
            return Ok(await _mappingAppService.AddAsync(mappingDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutMapping([FromBody] MappingDTO mappingDTO)
        {
            return Ok(await _mappingAppService.ModifyAsync(mappingDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapping([FromRoute] int id)
        {
            await _mappingAppService.Delete(id);
            return Ok();
        }

        [HttpGet("{id}/mapping_headers")]
        public async Task<IActionResult> GetMappingHeaders([FromRoute] int id)
        {
            return Ok(await _mappingAppService.GetMappingHeaders(id));
        }

        #endregion
    }
}
