using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Cashiers.Services;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CashiersController : AsyncBaseApiController<Cashier, CashierDTO>
    {
        #region Members
        private readonly ICashierAppService _cashierAppService;
        #endregion

        #region Constructor
        public CashiersController(ICashierAppService cashierAppService) : base(cashierAppService)
        {
            _cashierAppService = cashierAppService;
        }
        #endregion
    }
}
