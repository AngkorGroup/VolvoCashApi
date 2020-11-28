using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Application.MainContext.DocumentTypes.Services;
using VolvoCash.DistributedServices.Seedwork.Filters;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [Authorize(Roles = "Contact")]
    [ApiController]
    [Route("api_client/document_types")]
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
        public async Task<IActionResult> GetDocumentTypes()
        {
            var documentTypes = await _documentTypeAppService.GetDocumentTypes(onlyActive: true);
            return Ok(documentTypes);
        }
        #endregion
    }
}
