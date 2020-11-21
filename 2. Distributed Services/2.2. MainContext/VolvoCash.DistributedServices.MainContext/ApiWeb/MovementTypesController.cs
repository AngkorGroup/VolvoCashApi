using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.MovementTypes;
using VolvoCash.Application.MainContext.Movements.Services;
using VolvoCash.Application.MainContext.MovementTypes.Services;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class MovementTypesController: AsyncBaseApiController<MovementType,MovementTypeDTO>
    {
        #region Constructor
        public MovementTypesController(IMovementTypeAppService movementTypeAppService):base(movementTypeAppService)
        {
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
