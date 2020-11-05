using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Users.Services;
using VolvoCash.Application.MainContext.DTO.Users;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class UsersController : AsyncBaseApiController<User, UserDTO>
    {
        #region Members
        private readonly IUserAppService _userAppService;
        #endregion

        #region Constructor
        public UsersController(IUserAppService userAppService) : base(userAppService)
        {
            _userAppService = userAppService;
        }
        #endregion
    }
}
