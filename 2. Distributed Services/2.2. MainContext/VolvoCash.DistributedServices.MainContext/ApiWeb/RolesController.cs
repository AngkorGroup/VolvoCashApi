using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Roles.Services;
using VolvoCash.Application.MainContext.DTO.Roles;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class RolesController : ControllerBase
    {
        #region Members
        private readonly IRoleAppService _roleAppService;
        #endregion

        #region Constructor
        public RolesController(IRoleAppService roleAppService) 
        {
            _roleAppService = roleAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _roleAppService.GetRoles());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            return Ok(await _roleAppService.GetRole(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] RoleDTO roleDTO)
        {
            return Ok(await _roleAppService.AddAsync(roleDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutRole([FromBody] RoleDTO roleDTO)
        {
            return Ok(await _roleAppService.ModifyAsync(roleDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            await _roleAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
