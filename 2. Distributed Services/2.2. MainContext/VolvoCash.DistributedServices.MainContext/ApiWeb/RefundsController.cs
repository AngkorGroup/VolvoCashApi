using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.DTO.Refunds;
using VolvoCash.Application.MainContext.Refunds.Services;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.CrossCutting.Utils.Constants;
using VolvoCash.DistributedServices.MainContext.ApiWeb.Requests.Refunds;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class RefundsController : ControllerBase
    {
        #region Members
        private readonly IRefundAppService _refundAppService;
        #endregion

        #region Constructor
        public RefundsController(IRefundAppService refundAppService)
        {
            _refundAppService = refundAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetRefunds([FromQuery] string beginDate,
                                                    [FromQuery] string endDate, [FromQuery] string status)
        {
            var _beginDate = DateTimeParser.TryParseString(beginDate, DateTimeFormats.DateFormat);
            var _endDate = DateTimeParser.TryParseString(endDate, DateTimeFormats.DateFormat);
            if (_beginDate == null || _endDate == null) return Ok(new List<RefundDTO>());
            var refundStatus = (RefundStatus)EnumParser.ToEnum<RefundStatus>(status);
            var refunds = await _refundAppService.GetRefunds(_beginDate.Value, _endDate.Value, refundStatus);
            return Ok(refunds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRefund([FromRoute] int id)
        {
            return Ok(await _refundAppService.GetRefund(id));
        }

        [HttpGet("{id}/liquidations")]
        public async Task<IActionResult> GetRefundLiquidations([FromRoute] int id)
        {
            return Ok(await _refundAppService.GetRefundLiquidations(id));
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PayRefund([FromRoute] int id, [FromBody] PayRefundRequest request)
        {
            var paymentDate = DateTimeParser.ParseString(request.PaymentDate, DateTimeFormats.DateFormat);
            await _refundAppService.PayRefund(id, request.Voucher, paymentDate);
            return Ok();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelRefund([FromRoute] int id)
        {
            await _refundAppService.CancelRefund(id);
            return Ok();
        }

        [HttpPost("{id}/send")]
        public IActionResult SendSap([FromRoute] int id)
        {
            _refundAppService.SendSap(id);
            return Ok();
        }

        [HttpPost("{id}/resend")]
        public async Task<IActionResult> ResendSap([FromRoute] int id)
        {
            await _refundAppService.ResendSap(id);
            return Ok();
        }

        #endregion
    }
}
