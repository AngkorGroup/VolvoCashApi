using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Authentication.Services;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.DistributedServices.MainContext.ApiClient.Requests.Authentication;
using VolvoCash.DistributedServices.MainContext.ApiClient.Responses.Authentication;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.DistributedServices.Seedwork.Utils;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [ApiController]
    [AllowAnonymous]
    [Route("api_client")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class AuthenticationController : ControllerBase
    {
        #region Members
        private readonly IAuthenticationAppService _authenticationAppService;
        private readonly SMSManager _smsManager;
        private readonly TokenManager _tokenManager;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public AuthenticationController(IAuthenticationAppService authenticationAppService, IConfiguration configuration)
        {
            _authenticationAppService = authenticationAppService;
            _smsManager = new SMSManager(configuration);
            _tokenManager = new TokenManager(configuration);
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Public Methods
        [HttpPost]
        [Route("request_sms_code")]
        public async Task<ActionResult> RequestSmsCode([FromBody] RequestSmsCodeRequest request)
        {
            //TODO EVITAR ENVIAR MENSAJES MUY RAPIDOS QUIZAS HABILITADO SOLO EN PRODUCCION
            var code = await _authenticationAppService.RequestSmsCodeAsync(request.Phone);
            var message = $"{_resources.GetStringResource(LocalizationKeys.DistributedServices.messages_RequestCodeMessage)} {code}";
            _smsManager.Send(request.Phone, message);
            return Ok(new RequestSmsCodeResponse { IsValidPhoneNumber = true });
        }

        [HttpPost]
        [Route("verify_sms_code")]
        public async Task<ActionResult> VerifySmsCode([FromBody] VerifySmsCodeRequest request)
        {
            var contact = await _authenticationAppService.VerifySmsCodeAsync(request.Phone, int.Parse(request.Code));
            var authToken = _tokenManager.GenerateTokenJWT(contact.UserId,contact.Phone, contact.FullName, contact.Email, UserType.Contact.ToString());
            return Ok(new VerifySmsCodeResponse { AuthToken = authToken, Contact = contact });
        }

        [HttpDelete]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            //TODO Delete FCM TOKEN FROM SESSION
            return Ok();
        }
        #endregion
    }
}
