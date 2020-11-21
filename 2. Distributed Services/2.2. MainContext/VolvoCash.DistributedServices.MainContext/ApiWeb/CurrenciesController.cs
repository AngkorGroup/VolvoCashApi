using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Currencies.Services;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles="WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CurrenciesController : AsyncBaseApiController<Currency, CurrencyDTO>
    {
        #region Members
        #endregion

        #region Constructor
        public CurrenciesController(ICurrencyAppService currencyAppService) : base(currencyAppService)
        {
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
