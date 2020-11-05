using VolvoCash.Domain.Seedwork;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Users
{
    public class UserDTO : AuditableEntity
    {
        #region Properties
        public UserType Type { get; set; }
        #endregion
    }
}
