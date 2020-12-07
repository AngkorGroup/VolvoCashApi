using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Cashiers.Services;
using VolvoCash.Application.MainContext.Users.Services;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.DistributedServices.MainContext.ApiPOS.Requests.Authentication;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS
{
    [ApiController]
    [Authorize]
    [Route("api_pos/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ProfileController : ControllerBase
    {
        #region Members
        private readonly IUserAppService _userAppService;
        private readonly ICashierAppService _cashierAppService;     
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public ProfileController(IUserAppService userAppService,
                                 ICashierAppService cashierAppService,
                                 IApplicationUser applicationUser)
        {
            _userAppService = userAppService;
            _cashierAppService = cashierAppService;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            var cashier = await _cashierAppService.GetByUserId(_applicationUser.GetUserId());
            return Ok(cashier);
        }

        [HttpPost]
        [Route("change_password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            await _userAppService.ChangePassword(_applicationUser.GetUserId(),changePasswordRequest.OldPassword, changePasswordRequest.NewPassword,changePasswordRequest.ConfirmPassword);
            return Ok();
        }
        #endregion
    }
}
