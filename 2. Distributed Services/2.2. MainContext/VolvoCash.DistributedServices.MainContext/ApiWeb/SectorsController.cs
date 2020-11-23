using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Sectors.Services;
using VolvoCash.Application.MainContext.DTO.Sectors;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class SectorsController : ControllerBase
    {
        #region Members
        private readonly ISectorAppService _sectorAppService;
        #endregion

        #region Constructor
        public SectorsController(ISectorAppService sectorAppService) 
        {
            _sectorAppService = sectorAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetSectors([FromQuery] bool onlyActive = false)
        {
            return Ok(await _sectorAppService.GetSectors(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSector([FromRoute] int id)
        {
            return Ok(await _sectorAppService.GetSector(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostSector([FromBody] SectorDTO sectorDTO)
        {
            return Ok(await _sectorAppService.AddAsync(sectorDTO));
        }

        public async Task<IActionResult> PutSector([FromBody] SectorDTO sectorDTO)
        {
            return Ok(await _sectorAppService.ModifyAsync(sectorDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSector([FromRoute] int id)
        {
            await _sectorAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
