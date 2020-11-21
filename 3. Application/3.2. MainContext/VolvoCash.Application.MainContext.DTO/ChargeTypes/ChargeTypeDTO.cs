using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.ChargeTypes
{
    public class ChargeTypeDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }
        #endregion
    }
}
