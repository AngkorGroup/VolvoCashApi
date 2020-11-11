using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.CardTypes.Services;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class CardTypesController : AsyncBaseApiController<CardType, CardTypeDTO>
    {

        #region Constructor
        public CardTypesController(ICardTypeAppService cardTypeAppService) : base(cardTypeAppService)
        {
        }
        #endregion

        #region Public Methods
        [HttpPost]
        public override async Task<CardTypeDTO> Post([FromBody] CardTypeDTO entityDTO)
        {
            entityDTO.Status = Status.Active;
            return await _service.AddAsync(entityDTO);
        }
        [HttpDelete("{id}")]
        public override async Task Delete([FromRoute] int id)
        {
             await (_service as ICardTypeAppService).Delete(id);            
        }
        #endregion
    }
}
