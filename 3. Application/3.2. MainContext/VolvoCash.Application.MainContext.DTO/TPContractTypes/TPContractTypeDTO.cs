using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.TPContractTypes
{
    public class TPContractTypeDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
