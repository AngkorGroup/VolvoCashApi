using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Charges.Services;
using VolvoCash.Application.MainContext.Contacts.Services;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Transfers;
using VolvoCash.Application.MainContext.Loads.Services;
using VolvoCash.Application.MainContext.Transfers.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [Authorize]
    [ApiController]
    [Route("api_client")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MockController : ControllerBase
    {
        #region Members
        private readonly ILoadAppService _loadAppService;
        private readonly IContactAppService _contactAppService;
        private readonly ITransferAppService _transferAppService;
        private readonly IChargeAppService _chargeAppService;
        #endregion

        #region Constructor
        public MockController(ILoadAppService loadAppService,
                              IContactAppService contactAppService,
                              ITransferAppService transferAppService,
                              IChargeAppService chargeAppService)
        {
            _loadAppService = loadAppService;
            _contactAppService = contactAppService;
            _transferAppService = transferAppService;
            _chargeAppService = chargeAppService;
        }
        #endregion

        #region Public Methods

        [HttpPost]
        [AllowAnonymous]
        [Route("mock_contact")]
        public async Task<ActionResult> MockContact(ContactDTO request)
        {
            var contact = await _contactAppService.AddContact(request);
            return Ok(contact);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("mock_transfer")]
        public async Task<ActionResult> MockTransfer(TransferDTO request)
        {
            var transfer = await _transferAppService.PerformTransfer("", request);
            return Ok(transfer);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("mock_charge")]
        public async Task<ActionResult> MockCharge(ChargeDTO request)
        {
            var consumption = await _chargeAppService.AddCharge(request);
            return Ok(consumption);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("mock_confirm_charge")]
        public async Task<ActionResult> MockConfirmCharge(int chargeId, bool isConfirmed)
        {
            var consumption = await _chargeAppService.PerformChargeByPhone("", chargeId, isConfirmed);
            return Ok(consumption);
        }
        #endregion
    }
}
