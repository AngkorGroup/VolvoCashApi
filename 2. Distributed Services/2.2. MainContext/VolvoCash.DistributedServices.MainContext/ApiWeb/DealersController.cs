using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Dealers.Services;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
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
        [HttpPost]
        public override async Task<DealerDTO> Post([FromBody] DealerDTO entityDTO)
        {
            entityDTO.Status = Status.Active;
            return await _service.AddAsync(entityDTO);
        }
        [HttpDelete("{id}")]
        public override async Task Delete([FromRoute] int id)
        {
            await (_service as IDealerAppService).Delete(id);
        }
        #endregion
    }
}
