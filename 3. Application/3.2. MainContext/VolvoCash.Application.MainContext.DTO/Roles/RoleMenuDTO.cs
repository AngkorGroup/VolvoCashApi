
using VolvoCash.Application.MainContext.DTO.Menus;

namespace VolvoCash.Application.MainContext.DTO.Roles
{
    public class RoleMenuDTO
    {
        #region Properties
        public int Id { get; set; }

        public int RoleId { get; set; }

        public RoleDTO Role { get; set; }

        public int MenuId { get; set; }

        public MenuDTO Menu { get; set; }
        #endregion
    }
}
