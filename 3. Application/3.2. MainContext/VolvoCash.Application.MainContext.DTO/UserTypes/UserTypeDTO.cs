using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.UserTypes
{
    public class UserTypeDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
