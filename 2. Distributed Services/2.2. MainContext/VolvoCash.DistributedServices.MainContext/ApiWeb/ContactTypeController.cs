using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.ContactTypes.Services;
using VolvoCash.Application.MainContext.DTO.ContactTypes;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles= "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ContactTypeController:AsyncBaseApiController<ContactType,ContactTypeDTO>
    {
        #region Constructor
        public ContactTypeController(IContactTypeAppService contactTypeAppService):base(contactTypeAppService)
        {
        }
        #endregion

        #region Public Methods
        #endregion

    }
}
