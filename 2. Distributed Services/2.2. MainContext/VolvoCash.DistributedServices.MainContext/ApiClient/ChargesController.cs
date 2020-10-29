using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Charges.Services;
using VolvoCash.Application.MainContext.DTO.Charges.Requests;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.DistributedServices.Seedwork.Utils;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [Authorize(Roles = "Contact")]
    [ApiController]
    [Route("api_client/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ChargesController : Controller
    {
        #region Members
        private readonly IChargeAppService _chargeAppService;
        private readonly IUrlManager _urlManager;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public ChargesController(IChargeAppService chargeAppService, IUrlManager urlManager, IApplicationUser applicationUser)
        {
            _chargeAppService = chargeAppService;
            _urlManager = urlManager;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetCharges()
        {
            var charges = await _chargeAppService.GetChargesByPhone(_applicationUser.GetUserName());
            return Ok(charges);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCharge([FromRoute] int id)
        {
            var charge = await _chargeAppService.GetChargeByPhone(_applicationUser.GetUserName(), id);
            return Ok(charge);
        }

        [HttpPost("{id}/confirm")]
        public async Task<ActionResult> ConfirmCharge([FromRoute] int id, [FromBody] ConfirmChargeRequest request)
        {
            var charge = await _chargeAppService.PerformChargeByPhone(_applicationUser.GetUserName(), id, request.Confirmed);
            return Ok(charge);
        }

        [AllowAnonymous]
        [HttpGet("{id}/voucher")]
        public async Task<ActionResult> GetChargeVoucher([FromRoute] int id, [FromQuery] string operationCode)
        {
            var charge = await _chargeAppService.GetChargeById(id);
            charge.OperationCode = operationCode;
            return View("Voucher", charge);
        }

        [AllowAnonymous]
        [HttpGet("{id}/voucher_image")]
        public ActionResult GetChargeVoucherImage([FromRoute] int id, [FromQuery] string operationCode)
        {
            var url = _urlManager.GetChargeVoucherHtmlUrl(id, operationCode);
            var image = UrlToImage.DownloadContentAsImage(url);
            return File(image, "image/jpeg");
        }
        #endregion
    }
}
