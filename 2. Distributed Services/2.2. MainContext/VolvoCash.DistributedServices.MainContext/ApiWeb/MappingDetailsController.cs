using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Mappings;
using VolvoCash.Application.MainContext.MappingDetails.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/mapping_details")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MappingDetailsController : ControllerBase
    {
        #region Members
        private readonly IMappingDetailAppService _mappingDetailAppService;
        #endregion

        #region Constructor
        public MappingDetailsController(IMappingDetailAppService mappingDetailAppService)
        {
            _mappingDetailAppService = mappingDetailAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetMappingDetails([FromQuery] bool onlyActive = false)
        {
            return Ok(await _mappingDetailAppService.GetMappingDetails(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMappingDetail([FromRoute] int id)
        {
            return Ok(await _mappingDetailAppService.GetMappingDetail(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostMappingDetail([FromBody] MappingDetailDTO mappingDetailDTO)
        {
            return Ok(await _mappingDetailAppService.AddAsync(mappingDetailDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutMappingDetail([FromBody] MappingDetailDTO mappingDetailDTO)
        {
            return Ok(await _mappingDetailAppService.ModifyAsync(mappingDetailDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMappingDetail([FromRoute] int id)
        {
            await _mappingDetailAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
