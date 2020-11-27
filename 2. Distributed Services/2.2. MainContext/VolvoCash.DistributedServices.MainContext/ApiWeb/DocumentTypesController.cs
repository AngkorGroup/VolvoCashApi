using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.DocumentTypes.Services;
using VolvoCash.Application.MainContext.DTO.DocumentTypes;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/document_types")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class DocumentTypesController : ControllerBase
    {
        #region Members
        private readonly IDocumentTypeAppService _documentTypeAppService;
        #endregion

        #region Constructor
        public DocumentTypesController(IDocumentTypeAppService documentTypeAppService) 
        {
            _documentTypeAppService = documentTypeAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetDocumentTypes([FromQuery] bool onlyActive = false)
        {
            return Ok(await _documentTypeAppService.GetDocumentTypes(onlyActive));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentType([FromRoute] int id)
        {
            return Ok(await _documentTypeAppService.GetDocumentType(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostDocumentType([FromBody] DocumentTypeDTO documentTypeDTO)
        {
            return Ok(await _documentTypeAppService.AddAsync(documentTypeDTO));
        }

        [HttpPut]
        public async Task<IActionResult> PutDocumentType([FromBody] DocumentTypeDTO documentTypeDTO)
        {
            return Ok(await _documentTypeAppService.ModifyAsync(documentTypeDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentType([FromRoute] int id)
        {
            await _documentTypeAppService.Delete(id);
            return Ok();
        }
        #endregion
    }
}
