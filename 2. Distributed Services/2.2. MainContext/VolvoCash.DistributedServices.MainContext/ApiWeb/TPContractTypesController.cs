using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.TPContractTypes;
using VolvoCash.Application.MainContext.TPContractTypes.Services;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Aggregates.TPContractTypeAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class TPContractTypesController: AsyncBaseApiController<TPContractType,TPContractTypeDTO>
    {
        #region Constructor
        public TPContractTypesController(ITPContractTypeAppService tPContractTypeAppService):base(tPContractTypeAppService)
        {

        }
        #endregion

        #region Public Methods
        #endregion
    }
}
