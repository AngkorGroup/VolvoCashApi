using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.MovementTypes
{
    public class MovementTypeDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
