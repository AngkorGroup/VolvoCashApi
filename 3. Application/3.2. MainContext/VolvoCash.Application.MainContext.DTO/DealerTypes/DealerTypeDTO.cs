using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.DealerTypes
{
    public class DealerTypeDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
