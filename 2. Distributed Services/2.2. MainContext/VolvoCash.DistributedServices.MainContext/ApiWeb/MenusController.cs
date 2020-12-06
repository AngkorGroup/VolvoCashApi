using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Menus.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MenusController : ControllerBase
    {
        #region Members
        private readonly IMenuAppService _menuAppService;
        #endregion

        #region Constructor
        public MenusController(IMenuAppService menuAppService) 
        {
            _menuAppService = menuAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            return Ok(await _menuAppService.GetMenus());
        }
        #endregion
    }
}
