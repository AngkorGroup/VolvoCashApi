using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.RoleAgg
{
    public class Role : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public string Name { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
        #endregion

        #region Constructor
        public Role()
        {
        }

        public Role(string name, List<int> MenuIds)
        {
            Name = name;
            MenuIds.ForEach(menuId => RoleMenus.Add(new RoleMenu(Id, menuId)));
        }
        #endregion
    }
}
