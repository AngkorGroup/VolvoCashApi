using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Contacts.Services;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Application.MainContext.Users.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class UsersController : ControllerBase
    {
        #region Members
        private readonly IUserAppService _userAppService;
        private readonly IContactAppService _contactAppService;
        #endregion

        #region Constructor
        public UsersController(IUserAppService userAppService,
                               IContactAppService contactAppService)
        {
            _userAppService = userAppService;
            _contactAppService = contactAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetUsers(bool onlyActive = false)
        {
            var users = await _userAppService.GetAllDTOAsync(onlyActive);
            return Ok(users);
        }

        [HttpGet("{contactId}/cards")]
        public async Task<ActionResult> GetCards([FromRoute] int contactId)
        {
            var cards = await _contactAppService.GetContactCards(contactId);
            return Ok(cards);
        }

        [HttpPost("admins")]
        public async Task<ActionResult> PostAdmin([FromBody] AdminDTO adminDTO)
        {
            var admin = await _userAppService.AddAdminAsync(adminDTO);
            return Ok(admin);
        }

        [HttpPut("admins")]
        public async Task<IActionResult> PutAdmin([FromBody] AdminDTO adminDTO)
        {
            var admin = await _userAppService.ModifyAdminAsync(adminDTO);
            return Ok(admin);
        }

        [HttpPost("{userId}/reset_password")]
        public async Task<ActionResult> ResetUserPasswordAsync([FromRoute] int userId)
        {
            await _userAppService.ResetUserPasswordAsync(userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id, [FromQuery] int? contactId)
        {
            await _userAppService.DeleteUserAsync(id, contactId);
            return Ok();
        }
        #endregion
    }
}
