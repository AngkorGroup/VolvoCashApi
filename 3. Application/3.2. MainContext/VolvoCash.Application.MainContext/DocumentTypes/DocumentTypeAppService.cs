using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Application.MainContext.DTO.DocumentTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;

namespace VolvoCash.Application.MainContext.DocumentTypes
{
    public class DocumentTypeAppService:Service<DocumentType,DocumentTypeDTO>,IDocumentTypeAppService
    {
        #region Constructor
        public DocumentTypeAppService(IDocumentTypeRepository documentTypeRepository):base(documentTypeRepository)
        {

        }
        #endregion
    }
}
