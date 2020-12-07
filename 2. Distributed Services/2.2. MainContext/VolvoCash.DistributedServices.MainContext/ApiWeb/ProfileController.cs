using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Cashiers.Services;
using VolvoCash.Application.MainContext.Users.Services;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.DistributedServices.MainContext.ApiPOS.Requests.Authentication;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [ApiController]
    [Authorize]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ProfileController : ControllerBase
    {
        #region Members
        private readonly IUserAppService _userAppService;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public ProfileController(IUserAppService userAppService,
                                 ICashierAppService cashierAppService,
                                 IApplicationUser applicationUser)
        {
            _userAppService = userAppService;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods

        [HttpPost]
        [Route("change_password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            await _userAppService.ChangePassword(_applicationUser.GetUserId(),changePasswordRequest.OldPassword, changePasswordRequest.NewPassword,changePasswordRequest.ConfirmNewPassword);
            return Ok();
        }
        #endregion
    }
}
