using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DocumentTypes;
using VolvoCash.Application.MainContext.DTO.DocumentTypes;
using VolvoCash.DistributedServices.Seedwork.Controllers;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "WebAdmin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class DocumentTypesController: AsyncBaseApiController<DocumentType,DocumentTypeDTO>
    {
        #region Constructor
        public DocumentTypesController(IDocumentTypeAppService documentTypeAppService):base(documentTypeAppService)
        {
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
