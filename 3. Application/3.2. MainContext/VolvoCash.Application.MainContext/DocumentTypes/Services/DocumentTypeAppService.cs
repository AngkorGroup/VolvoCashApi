using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.DocumentTypes;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DocumentTypes.Services
{
    public class DocumentTypeAppService : IDocumentTypeAppService
    {
        #region Members
        private readonly IDocumentTypeRepository _documentTypeRepository;
        #endregion

        #region Constructor
        public DocumentTypeAppService(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<DocumentTypeDTO>> GetDocumentTypes(bool onlyActive)
        {
            var documentTypes = await _documentTypeRepository.FilterAsync(filter: dt => !onlyActive || dt.Status == Status.Active);
            return documentTypes.ProjectedAsCollection<DocumentTypeDTO>();
        }

        public async Task<DocumentTypeDTO> GetDocumentType(int id)
        {
            var documentType = await _documentTypeRepository.GetAsync(id);
            return documentType.ProjectedAs<DocumentTypeDTO>();
        }

        public async Task<DocumentTypeDTO> AddAsync(DocumentTypeDTO documentTypeDTO)
        {
            var documentType = new DocumentType(
                documentTypeDTO.Name,
                documentTypeDTO.Abbreviation,
                documentTypeDTO.SUNATCode,
                documentTypeDTO.TPCode
            );
            _documentTypeRepository.Add(documentType);
            await _documentTypeRepository.UnitOfWork.CommitAsync();
            return documentType.ProjectedAs<DocumentTypeDTO>();
        }

        public async Task<DocumentTypeDTO> ModifyAsync(DocumentTypeDTO documentTypeDTO)
        {
            var documentType = await _documentTypeRepository.GetAsync(documentTypeDTO.Id);
            documentType.Name = documentTypeDTO.Name;
            documentType.Abbreviation = documentTypeDTO.Abbreviation;
            documentType.SUNATCode = documentTypeDTO.SUNATCode;
            documentType.TPCode = documentTypeDTO.TPCode;
            await _documentTypeRepository.UnitOfWork.CommitAsync();
            return documentTypeDTO;
        }

        public async Task Delete(int id)
        {
            var documentType = await _documentTypeRepository.GetAsync(id);
            documentType.ArchiveAt = DateTime.Now;
            documentType.Status = Status.Inactive;
            _documentTypeRepository.Modify(documentType);
            await _documentTypeRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _documentTypeRepository.Dispose();
        }
        #endregion
    }
}
