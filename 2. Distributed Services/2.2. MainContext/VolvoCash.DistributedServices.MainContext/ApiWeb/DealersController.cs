using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Dealers.Services;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.StatusAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class DealersController : AsyncBaseApiController<Dealer, DealerDTO>
    {
        #region Members
        private readonly IDealerAppService _dealerAppService;
        #endregion

        #region Constructor
        public DealersController(IDealerAppService dealerAppService) : base(dealerAppService)
        {
            _dealerAppService = dealerAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet("by_filter")]
        public async Task<ActionResult> GetDealers([FromQuery] string query = "", int maxRecords = 5 )
        {
            var charges = await _dealerAppService.GetDealers(query, maxRecords);
            return Ok(charges);
        }

        [HttpGet("{id}/cashiers")]
        public async Task<ActionResult> GetDealerCashiers([FromRoute] int id)
        {
            var charges = await _dealerAppService.GetDealerCashiers(id);
            return Ok(charges);
        }

        [HttpGet("{id}/charges")]
        public async Task<ActionResult> GetDealerCharges([FromRoute] int id,[FromQuery] string beginDate = "", [FromQuery] string endDate = "",
                                                        [FromQuery] int? cashierId =null, [FromQuery] string cardTypes = ""
                                                    )
        {
            DateTime? bDate = null;
            if (!string.IsNullOrEmpty(beginDate))
            {
                bDate =  DateTime.ParseExact(beginDate, DateTimeFormats.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            }
            DateTime? eDate = null;
            if (!string.IsNullOrEmpty(endDate))
            {
                eDate = DateTime.ParseExact(endDate, DateTimeFormats.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            }
            var cardTypesList = string.IsNullOrEmpty(cardTypes) ? new List<int>() : cardTypes.Split(",").Select(s => int.Parse(s)).ToList();
            var charges = await _dealerAppService.GetDealerCharges(id, bDate, eDate, cashierId, cardTypesList);
            return Ok(charges);
        }

        [HttpPost]
        public override async Task<DealerDTO> Post([FromBody] DealerDTO entityDTO)
        {
            entityDTO.Status = new Status(1);
            return await _service.AddAsync(entityDTO);
        }

        [HttpDelete("{id}")]
        public override async Task Delete([FromRoute] int id)
        {
            await _dealerAppService.Delete(id);
        }
        #endregion
    }
}
