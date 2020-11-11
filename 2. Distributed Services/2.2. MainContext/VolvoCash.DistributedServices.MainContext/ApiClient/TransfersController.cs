using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Transfers;
using VolvoCash.Application.MainContext.Transfers.Services;
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
    public class TransfersController : Controller
    {
        #region Members
        private readonly ITransferAppService _transferAppService;
        private readonly IUrlManager _urlManager;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public TransfersController(ITransferAppService transferAppService, 
                                   IUrlManager urlManager,
                                   IApplicationUser applicationUser)
        {
            _transferAppService = transferAppService;
            _urlManager = urlManager;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetTransfers()
        {
            var transfers = await _transferAppService.GetTransfersByPhone(_applicationUser.GetUserName());
            return Ok(transfers);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransfer(TransferDTO request)
        {
            var transfer = await _transferAppService.PerformTransfer(_applicationUser.GetUserName(), request);
            return Ok(transfer);
        }

        [AllowAnonymous]
        [HttpGet("{id}/voucher")]
        public async Task<ActionResult> GetTransferVoucher([FromRoute] int id)
        {
            var charge = await _transferAppService.GetTransferById(id);
            return View("Voucher", charge);
        }

        [AllowAnonymous]
        [HttpGet("{id}/voucher_image")]
        public ActionResult GetTransferVoucherImage([FromRoute] int id)
        {
            var url = _urlManager.GetTransferVoucherHtmlUrl(id);
            var image = UrlToImage.DownloadContentAsImage(url);
            return File(image, "image/jpeg");
        }
        #endregion
    }
}
