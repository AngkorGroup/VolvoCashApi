using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Charges.Services;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.EnumAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [Authorize]
    [ApiController]
    [Route("api_client")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MockController : ControllerBase
    {
        #region Members
        private readonly IChargeAppService _chargeAppService;
        #endregion

        #region Constructor
        public MockController(IChargeAppService chargeAppService)
        {
            _chargeAppService = chargeAppService;
        }
        #endregion

        #region Public Methods
        [HttpPost]
        [AllowAnonymous]
        [Route("mock_charge")]
        public async Task<ActionResult> MockCharge(ChargeDTO request)
        {
            request.Amount = new MoneyDTO(new Currency("Dolar Estadounidense",'$',"USD","###"), 100);
            request.Description = "Esto es una prueba de un consumo";
            request.CashierId = 1;
            request.ChargeType = new ChargeType("FaceToFace","###",2);
            request.CardId = JsonConvert.DeserializeObject<CardDTO>(CryptoMethods.DecryptString(request.CardToken)).Id;
            var charge = await _chargeAppService.AddCharge(request);
            return Ok(charge);
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
