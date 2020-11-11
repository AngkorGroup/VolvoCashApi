using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Authentication.Services;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.DistributedServices.MainContext.ApiPOS.Requests.Authentication;
using VolvoCash.DistributedServices.MainContext.ApiPOS.Responses.Authentication;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.DistributedServices.Seedwork.Utils;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS
{
    [ApiController]
    [AllowAnonymous]
    [Route("api_pos")]
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
            var cashier = await _authenticationAppService.LoginCashierAsync(request.Email, request.Password);
            var session = await _authenticationAppService.CreateSessionAsync(cashier.UserId, request.DeviceToken);
            var authToken = _tokenManager.GenerateTokenJWT(session.Id,cashier.UserId, cashier.Id.ToString(), cashier.FullName, cashier.Email, UserType.Cashier.ToString());
            return Ok(new LoginResponse { Cashier = cashier, AuthToken = authToken });
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
