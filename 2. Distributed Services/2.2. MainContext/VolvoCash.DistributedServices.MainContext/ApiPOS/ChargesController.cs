using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Charges.Services;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS
{
    [Authorize(Roles = "Cashier")]
    [ApiController]
    [Route("api_pos/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ChargesController : ControllerBase
    {
        #region Members
        private readonly IChargeAppService _chargeAppService;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public ChargesController(IChargeAppService chargeAppService,
                                 IApplicationUser applicationUser)
        {
            _chargeAppService = chargeAppService;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetCharges([FromQuery] ChargeType chargeType, [FromQuery] int pageIndex = 0, [FromQuery] int pageLength = 10)
        {
            var charges = await _chargeAppService.GetChargesByCashierId(int.Parse(_applicationUser.GetUserName()), chargeType, pageIndex, pageLength);
            return Ok(charges);
        }

        [HttpPost]
        public async Task<ActionResult> AddCharge([FromBody] ChargeDTO request)
        {
            request.CashierId = int.Parse(_applicationUser.GetUserName());
            request.CardId = JsonConvert.DeserializeObject<CardDTO>(CryptoMethods.DecryptString(request.CardToken)).Id;
            var charge = await _chargeAppService.AddCharge(request);
            return Ok(charge);
        }
        #endregion
    }
}
