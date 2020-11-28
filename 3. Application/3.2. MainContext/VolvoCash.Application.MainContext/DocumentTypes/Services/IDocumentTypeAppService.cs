using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.DocumentTypes;

namespace VolvoCash.Application.MainContext.DocumentTypes.Services
{
    public interface IDocumentTypeAppService : IDisposable
    {
        #region ApiWeb
        Task<List<DocumentTypeDTO>> GetDocumentTypes(bool onlyActive);
        Task<DocumentTypeDTO> GetDocumentType(int id);
        Task<DocumentTypeDTO> AddAsync(DocumentTypeDTO documentTypeDTO);
        Task<DocumentTypeDTO> ModifyAsync(DocumentTypeDTO documentTypeDTO);
        Task Delete(int id);
        #endregion
    }
}
