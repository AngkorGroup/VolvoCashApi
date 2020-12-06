
using System.Collections.Generic;

namespace VolvoCash.Application.MainContext.DTO.Roles
{
    public class RoleDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public List<RoleMenuDTO> RoleMenus { get; }

        public List<int> MenuIds { get; }
        #endregion
    }
}
