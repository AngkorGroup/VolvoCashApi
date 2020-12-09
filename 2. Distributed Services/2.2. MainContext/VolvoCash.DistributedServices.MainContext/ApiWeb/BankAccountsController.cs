using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.BankAccounts.Services;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/bank_accounts")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BankAccountsController : ControllerBase
    {
        #region Members
        private readonly IBankAccountAppService _bankAccountAppService;
        #endregion

        #region Constructor
        public BankAccountsController(IBankAccountAppService bankAccountAppService)
        {
            _bankAccountAppService = bankAccountAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetBankAccounts([FromQuery] bool onlyActive = false)
        {
            return Ok(await _bankAccountAppService.GetBankAccounts(onlyActive));
        }

        [HttpGet("volvo")]
        public async Task<IActionResult> GetBankAccountsForVolvo([FromQuery] bool onlyActive = false)
        {
            return Ok(await _bankAccountAppService.GetBankAccountsForVolvo(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankAccount([FromRoute] int id)
        {
            return Ok(await _bankAccountAppService.GetBankAccount(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBankAccount([FromBody] BankAccountDTO bankAccountDTO)
        {
            return Ok(await _bankAccountAppService.AddAsync(bankAccountDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutBank([FromBody] BankAccountDTO bankAccountDTO)
        {
            return Ok(await _bankAccountAppService.ModifyAsync(bankAccountDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank([FromRoute] int id)
        {
            await _bankAccountAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
