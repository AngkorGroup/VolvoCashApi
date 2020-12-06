using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.MenuAgg
{
    public class Menu : Entity
    {
        #region Properties
        [Required]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        public MenuType Type { get; set; }

        public Status Status { get; set; }

        public string Icon { get; set; }

        public int? Order { get; set; }

        [ForeignKey("MenuParent")]
        public int? MenuParentId { get; set; }

        public virtual Menu MenuParent { get; set; }

        public virtual ICollection<Menu> MenuChildren { get; set; } = new List<Menu>();

        public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
        #endregion

        #region NotMapped Properties
        [NotMapped]
        public string DisplayName { get => $"{MenuParent.Name}: {Name}"; }
        #endregion

        #region Constructor
        public Menu()
        {
        }
        #endregion
    }
}
