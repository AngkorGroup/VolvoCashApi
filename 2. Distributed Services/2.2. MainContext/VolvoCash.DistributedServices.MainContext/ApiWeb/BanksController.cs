using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.Banks.Services;
using VolvoCash.Application.MainContext.DTO.Banks;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BanksController : ControllerBase
    {
        #region Members
        private readonly IBankAppService _bankAppService;
        #endregion

        #region Constructor
        public BanksController(IBankAppService bankAppService)
        {
            _bankAppService = bankAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetBanks([FromQuery] bool onlyActive = false)
        {
            return Ok(await _bankAppService.GetBanks(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBank([FromRoute] int id)
        {
            return Ok(await _bankAppService.GetBank(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBank([FromBody] BankDTO bankDTO)
        {
            return Ok(await _bankAppService.AddAsync(bankDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutBank([FromBody] BankDTO bankDTO)
        {
            return Ok(await _bankAppService.ModifyAsync(bankDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank([FromRoute] int id)
        {
            await _bankAppService.Delete(id);
            return Ok();
        }

        [HttpGet("{id}/accounts")]
        public async Task<IActionResult> GetBankAccounts([FromRoute] int id)
        {
            return Ok(await _bankAppService.GetBankAccounts(id));
        }

        #endregion
    }
}
