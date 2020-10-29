using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public class User : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public UserType Type { get; set; }
        #endregion

        #region Constructor
        public User()
        {
        }

        public User(UserType type)
        {
            Type = type;
        }
        #endregion
    }
}
