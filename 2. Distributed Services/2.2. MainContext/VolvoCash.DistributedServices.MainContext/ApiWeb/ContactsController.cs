using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Contacts.Services;
using VolvoCash.Application.MainContext.DTO.Contacts;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
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
        [HttpGet("by_client")]
        public async Task<ActionResult> GetContactsByClientId([FromQuery] int clientId, [FromQuery] bool onlyActive = false)
        {
            var contacts = await _contactAppService.GetContactsByClientId(clientId, onlyActive);
            return Ok(contacts);
        }

        [HttpGet("by_filter")]
        public async Task<ActionResult> GetContactsByFilter([FromQuery] string query, [FromQuery] int maxRecords = 5, [FromQuery] bool onlyActive = false)
        {
            var contacts = await _contactAppService.GetContactsByFilter(query, maxRecords, onlyActive);
            return Ok(contacts);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContact([FromBody] ContactDTO contactDTO)
        {
            var contact = await _contactAppService.UpdateContact(contactDTO);
            return Ok(contact);
        }

        [HttpPost("{id}/make_primary")]
        public async Task<ActionResult> MakeContactAsPrimary([FromRoute] int id)
        {
            await _contactAppService.MakeContactAsPrimary(id);
            return Ok();
        }
        #endregion
    }
}
