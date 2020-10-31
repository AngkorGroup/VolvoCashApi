using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Contacts.Services;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ContactsController : ControllerBase
    {
        #region Members
        private readonly IContactAppService _contactAppService;
        #endregion

        #region Constructor
        public ContactsController(IContactAppService contactAppService)
        {
            _contactAppService = contactAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetContactsByClientId([FromQuery] int clientId)
        {
            var contacts = await _contactAppService.GetContactsByClientId(clientId);
            return Ok(contacts);
        }
        #endregion
    }
}
