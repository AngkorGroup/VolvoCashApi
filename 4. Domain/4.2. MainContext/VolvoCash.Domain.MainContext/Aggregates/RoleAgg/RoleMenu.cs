using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.MenuAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.RoleAgg
{
    public class RoleMenu : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        [Required]
        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        #endregion

        #region Constructor
        public RoleMenu()
        {
        }

        public RoleMenu(int roleId, int menuId)
        {
            RoleId = roleId;
            MenuId = menuId;
        }
        #endregion
    }
}
