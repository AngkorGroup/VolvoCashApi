using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.DocumentTypes
{
    public class DocumentTypeDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }
        
        public string Descrpition { get; set; }
        #endregion
    }
}
