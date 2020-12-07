using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.RoleAgg
{
    public class RoleAdmin : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        [Required]
        [ForeignKey("Admin")]
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
        #endregion

        #region Constructor
        public RoleAdmin()
        {
        }

        public RoleAdmin(int roleId, int adminId)
        {
            RoleId = roleId;
            AdminId = adminId;
        }
        #endregion
    }
}
