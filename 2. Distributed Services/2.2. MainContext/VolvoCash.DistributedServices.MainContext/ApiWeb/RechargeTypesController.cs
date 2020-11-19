using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.RechargeTypes.Services;
using VolvoCash.Application.MainContext.DTO.RechargeTypes;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/recharge_types")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class RechargeTypesController : ControllerBase
    {
        #region Members
        private readonly IRechargeTypeAppService _rechargeTypeAppService;
        #endregion

        #region Constructor
        public RechargeTypesController(IRechargeTypeAppService rechargeTypeAppService) 
        {
            _rechargeTypeAppService = rechargeTypeAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetRechargeTypes([FromQuery] bool onlyActive = false)
        {
            return Ok(await _rechargeTypeAppService.GetRechargeTypes(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRechargeType([FromRoute] int id)
        {
            return Ok(await _rechargeTypeAppService.GetRechargeType(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostRechargeType([FromBody] RechargeTypeDTO rechargeTypeDTO)
        {
            return Ok(await _rechargeTypeAppService.AddAsync(rechargeTypeDTO));
        }

        public async Task<IActionResult> PutRechargeType([FromBody] RechargeTypeDTO rechargeTypeDTO)
        {
            return Ok(await _rechargeTypeAppService.ModifyAsync(rechargeTypeDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRechargeType([FromRoute] int id)
        {
            await _rechargeTypeAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
