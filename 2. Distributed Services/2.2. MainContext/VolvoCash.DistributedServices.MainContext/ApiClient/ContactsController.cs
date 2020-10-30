using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Contacts.Services;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [Authorize(Roles = "Contact")]
    [ApiController]
    [Route("api_client/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ContactsController : ControllerBase
    {
        #region Members
        private readonly IContactAppService _contactAppService;
        private readonly IApplicationUser _applicationUser;
        #endregion

        #region Constructor
        public ContactsController(IContactAppService contactAppService,
                                  IApplicationUser applicationUser)
        {
            _contactAppService = contactAppService;
            _applicationUser = applicationUser;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            var contacts = await _contactAppService.GetContactsByPhone(_applicationUser.GetUserName());
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<ActionResult> AddContact(ContactDTO request)
        {
            //TODO VALIDAR QUE EL TELEFONO SEA VALIDO
            request.ContactParent = new ContactListDTO() { Phone = _applicationUser.GetUserName() };
            var contact = await _contactAppService.AddContact(request);
            return Ok(contact);
        }
        #endregion
    }
}
