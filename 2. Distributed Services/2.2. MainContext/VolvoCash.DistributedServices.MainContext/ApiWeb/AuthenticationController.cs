using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Authentication.Services;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Authentication;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Responses.Authentication;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.DistributedServices.Seedwork.Utils;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [ApiController]
    [AllowAnonymous]
    [Route("api_web")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class AuthenticationController : ControllerBase
    {
        #region Members
        private readonly IAuthenticationAppService _authenticationAppService;
        private readonly TokenManager _tokenManager;
        #endregion

        #region Constructor
        public AuthenticationController(IAuthenticationAppService authenticationAppService,
                                        IConfiguration configuration)
        {
            _authenticationAppService = authenticationAppService;
            _tokenManager = new TokenManager(configuration);
        }
        #endregion

        #region Public Methods
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var admin = await _authenticationAppService.LoginAdminAsync(request.Email, request.Password);
            var authToken = _tokenManager.GenerateTokenJWT(admin.UserId, admin.Id.ToString(), admin.FullName, admin.Email, UserType.Admin.ToString());
            return Ok(new LoginResponse { Admin = admin, AuthToken = authToken });
        }

        [HttpDelete]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            return Ok();
        }
        #endregion
    }
}
