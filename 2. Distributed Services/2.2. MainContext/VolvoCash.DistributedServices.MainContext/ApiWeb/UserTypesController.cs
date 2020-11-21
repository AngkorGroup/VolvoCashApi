using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.UserTypes;
using VolvoCash.Application.MainContext.UserTypes.Services;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class UserTypesController: AsyncBaseApiController<UserType,UserTypeDTO>
    {
        #region Constructor
        public UserTypesController(IUserTypeAppService userTypeAppService):base(userTypeAppService)
        {
                
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
