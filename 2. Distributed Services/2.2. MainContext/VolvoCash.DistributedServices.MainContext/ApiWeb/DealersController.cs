using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Dealers.Services;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.CrossCutting.Utils.Constants;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class DealersController : ControllerBase
    {
        #region Members
        private readonly IDealerAppService _dealerAppService;
        #endregion

        #region Constructor
        public DealersController(IDealerAppService dealerAppService)
        {
            _dealerAppService = dealerAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetDealers([FromQuery] bool onlyActive = false)
        {
            return Ok(await _dealerAppService.GetDealers(onlyActive));
        }

        [HttpGet("by_filter")]
        public async Task<ActionResult> GetDealers([FromQuery] string query = "", int maxRecords = 50)
        {
            var charges = await _dealerAppService.GetDealers(query, maxRecords);
            return Ok(charges);
        }

        [HttpGet("{id}/cashiers")]
        public async Task<ActionResult> GetDealerCashiers([FromRoute] int id, [FromQuery] bool onlyActive = false)
        {
            var charges = await _dealerAppService.GetDealerCashiers(id, onlyActive);
            return Ok(charges);
        }

        [HttpGet("{id}/charges")]
        public async Task<ActionResult> GetDealerCharges([FromRoute] int id, [FromQuery] string beginDate = "",
                                                         [FromQuery] string endDate = "", [FromQuery] int? cashierId = null,
                                                         [FromQuery] string cardTypes = "")
        {
            var _beginDate = DateTimeParser.TryParseString(beginDate, DateTimeFormats.DateFormat);
            var _endDate = DateTimeParser.TryParseString(endDate, DateTimeFormats.DateFormat);
            var cardTypesList = string.IsNullOrEmpty(cardTypes) ? new List<int>() : cardTypes.Split(",").Select(s => int.Parse(s)).ToList();
            var charges = await _dealerAppService.GetDealerCharges(id, _beginDate, _endDate, cashierId, cardTypesList);
            return Ok(charges);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DealerDTO dealerDTO)
        {
            dealerDTO.Status = Status.Active;
            return Ok(await _dealerAppService.AddAsync(dealerDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutDealer([FromBody] DealerDTO dealerDTO)
        {
            return Ok(await _dealerAppService.ModifyAsync(dealerDTO));
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _dealerAppService.Delete(id);
        }

        [HttpGet("{id}/bank_accounts")]
        public async Task<ActionResult> GetBankAccounts([FromRoute] int id, [FromQuery] bool onlyActive = false)
        {
            var bankAccounts = await _dealerAppService.GetBankAccounts(id, onlyActive);
            return Ok(bankAccounts);
        }
        #endregion
    }
}
