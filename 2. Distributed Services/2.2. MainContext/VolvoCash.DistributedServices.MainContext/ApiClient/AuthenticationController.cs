﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Authentication.Services;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.CrossCutting.NetFramework.Utils;
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
        private readonly ISMSManager _smsManager;
        private readonly ITokenManager _tokenManager;
        private readonly IApplicationUser _applicationUser;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public AuthenticationController(IAuthenticationAppService authenticationAppService,
                                        ISMSManager smsManager,
                                        ITokenManager tokenManager,
                                        IApplicationUser applicationUser)
        {
            _authenticationAppService = authenticationAppService;
            _smsManager = smsManager;
            _tokenManager = tokenManager;
            _applicationUser = applicationUser;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Public Methods
        [HttpPost]
        [Route("request_sms_code")]
        public async Task<ActionResult> RequestSmsCode([FromBody] RequestSmsCodeRequest request)
        {
            var code = await _authenticationAppService.RequestSmsCodeAsync(request.Phone);            
            var message = $"{_resources.GetStringResource(LocalizationKeys.DistributedServices.messages_RequestCodeMessage)} {code}";
            _smsManager.SendSMS(request.Phone, message);
            return Ok(new RequestSmsCodeResponse { IsValidPhoneNumber = true, SmsCode = code });
        }

        [HttpPost]
        [Route("verify_sms_code")]
        public async Task<ActionResult> VerifySmsCode([FromBody] VerifySmsCodeRequest request)
        {
            var contact = await _authenticationAppService.VerifySmsCodeAsync(request.Phone, int.Parse(request.Code));
            var session = await _authenticationAppService.CreateSessionAsync(contact.UserId, request.DeviceToken);
            var authToken = _tokenManager.GenerateTokenJWT(session.Id, contact.UserId, contact.Phone, contact.FullName, contact.Email, UserType.Contact.ToString());
            contact.Type = ContactType.Primary;
            return Ok(new VerifySmsCodeResponse { AuthToken = authToken, Contact = contact });
        }

        [HttpDelete]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _authenticationAppService.DestroySessionAsync(_applicationUser.GetSessionId());
            } catch 
            {
            }            
            return Ok();
        }
        #endregion
    }
}
