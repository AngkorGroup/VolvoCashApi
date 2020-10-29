using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Contacts.Services;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiPOS
{
    [Authorize(Roles = "Cashier")]
    [ApiController]
    [Route("api_pos/[controller]")]
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
        public async Task<ActionResult> GetContacts([FromQuery] string query = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageLength = 10)
        {
            var contacts = await _contactAppService.GetContacts(query, pageIndex, pageLength);
            return Ok(contacts);
        }

        [HttpGet]
        [Route("{id}/cards")]
        public async Task<ActionResult> GetContactCards([FromRoute] int id)
        {
            var cards = await _contactAppService.GetContactCards(id);
            cards.ForEach(card => card.CardToken = CardTokenizer.GetCardToken(card.GetCardForQr()));
            return Ok(cards);
        }
        #endregion
    }
}
