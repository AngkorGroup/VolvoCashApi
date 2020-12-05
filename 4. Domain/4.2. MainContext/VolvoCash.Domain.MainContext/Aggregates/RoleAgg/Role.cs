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

        public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
        #endregion

        #region Constructor
        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
        #endregion
    }
}
