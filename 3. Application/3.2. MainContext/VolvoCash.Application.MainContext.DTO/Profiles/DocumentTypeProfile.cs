using AutoMapper;
using VolvoCash.Application.MainContext.DTO.DocumentTypes;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;

namespace VolvoCash.Application.MainContext.DTO.Profiles
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<DocumentType, DocumentTypeDTO>().PreserveReferences();
        }
    }
}
