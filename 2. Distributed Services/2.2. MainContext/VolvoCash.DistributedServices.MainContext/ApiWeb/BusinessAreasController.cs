using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.BusinessAreas.Services;
using VolvoCash.Application.MainContext.DTO.BusinessAreas;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/business_areas")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BusinessAreasController : ControllerBase
    {
        #region Members
        private readonly IBusinessAreaAppService _businessAreaAppService;
        #endregion

        #region Constructor
        public BusinessAreasController(IBusinessAreaAppService businessAreaAppService) 
        {
            _businessAreaAppService = businessAreaAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetBusinessAreas([FromQuery] bool onlyActive = false)
        {
            return Ok(await _businessAreaAppService.GetBusinessAreas(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessArea([FromRoute] int id)
        {
            return Ok(await _businessAreaAppService.GetBusinessArea(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBusinessArea([FromBody] BusinessAreaDTO businessAreaDTO)
        {
            return Ok(await _businessAreaAppService.AddAsync(businessAreaDTO));
        }

        public async Task<IActionResult> PutBusinessArea([FromBody] BusinessAreaDTO businessAreaDTO)
        {
            return Ok(await _businessAreaAppService.ModifyAsync(businessAreaDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessArea([FromRoute] int id)
        {
            await _businessAreaAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
