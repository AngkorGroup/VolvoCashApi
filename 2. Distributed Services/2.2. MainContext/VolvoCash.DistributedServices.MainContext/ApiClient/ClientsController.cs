using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Clients.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.CrossCutting.NetFramework.Identity;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [Authorize(Roles = "Contact")]
    [ApiController]
    [Route("api_client/document_types")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ClientsController : ControllerBase
    {
        #region Members
        private readonly IClientAppService _clientAppService;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public ClientsController(IClientAppService clientAppService, IApplicationUser applicationUser)
        {
            _clientAppService = clientAppService;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var Clients = await _clientAppService.GetClientsByPhone(_applicationUser.GetUserName());
            return Ok(Clients);
        }
        #endregion
    }
}
