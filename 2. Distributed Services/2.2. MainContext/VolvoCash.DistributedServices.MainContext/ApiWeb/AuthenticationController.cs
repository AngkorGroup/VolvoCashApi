using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Authentication.Services;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Authentication;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Responses.Authentication;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.DistributedServices.Seedwork.Utils;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

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
        private readonly ITokenManager _tokenManager;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public AuthenticationController(IAuthenticationAppService authenticationAppService,
                                        ITokenManager tokenManager,
                                        IApplicationUser applicationUser)
        {
            _authenticationAppService = authenticationAppService;
            _tokenManager = tokenManager;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var admin = await _authenticationAppService.LoginAdminAsync(request.Email, request.Password);
            var session = await _authenticationAppService.CreateSessionAsync(admin.UserId);
            var userType = new UserType("WebAdmin","###");
            var authToken = _tokenManager.GenerateTokenJWT(session.Id, admin.UserId, admin.Id.ToString(), admin.FullName, admin.Email, userType.Name);
            return Ok(new LoginResponse { Admin = admin, AuthToken = authToken });
        }

        [HttpDelete]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await _authenticationAppService.DestroySessionAsync(_applicationUser.GetSessionId());
            return Ok();
        }
        #endregion
    }
}
