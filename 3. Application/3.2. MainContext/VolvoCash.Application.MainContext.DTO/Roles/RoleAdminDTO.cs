using VolvoCash.Application.MainContext.DTO.Admins;

namespace VolvoCash.Application.MainContext.DTO.Roles
{
    public class RoleAdminDTO
    {
        #region Properties
        public int Id { get; set; }

        public int RoleId { get; set; }

        public RoleDTO Role { get; set; }

        public int AdminId { get; set; }

        //public AdminDTO Admin { get; set; }
        #endregion
    }
}
