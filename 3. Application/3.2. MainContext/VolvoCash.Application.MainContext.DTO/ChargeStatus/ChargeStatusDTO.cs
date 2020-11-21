using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.ChargeStatus
{
    public class ChargeStatusDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; 
        }
        public string Description { get; set; }

        public int Weight { get; set; }
        #endregion
    }
}
