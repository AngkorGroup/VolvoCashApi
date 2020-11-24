using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.BankAccountTypes.Services;
using VolvoCash.Application.MainContext.DTO.BankAccountTypes;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/bank_account_types")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BankAccountTypesController : ControllerBase
    {
        #region Members
        private readonly IBankAccountTypeAppService _bankAccountTypeAppService;
        #endregion

        #region Constructor
        public BankAccountTypesController(IBankAccountTypeAppService bankAccountTypeAppService) 
        {
            _bankAccountTypeAppService = bankAccountTypeAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetBankAccountTypes([FromQuery] bool onlyActive = false)
        {
            return Ok(await _bankAccountTypeAppService.GetBankAccountTypes(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankAccountType([FromRoute] int id)
        {
            return Ok(await _bankAccountTypeAppService.GetBankAccountType(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostBankAccountType([FromBody] BankAccountTypeDTO bankAccountTypeDTO)
        {
            return Ok(await _bankAccountTypeAppService.AddAsync(bankAccountTypeDTO));
        }

        public async Task<IActionResult> PutBankAccountType([FromBody] BankAccountTypeDTO bankAccountTypeDTO)
        {
            return Ok(await _bankAccountTypeAppService.ModifyAsync(bankAccountTypeDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccountType([FromRoute] int id)
        {
            await _bankAccountTypeAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
