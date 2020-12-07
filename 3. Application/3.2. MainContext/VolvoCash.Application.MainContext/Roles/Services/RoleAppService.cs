using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Roles;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;

namespace VolvoCash.Application.MainContext.Roles.Services
{
    public class RoleAppService : IRoleAppService
    {
        #region Members
        private readonly IRoleRepository _roleRepository;
        #endregion

        #region Constructor
        public RoleAppService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _roleRepository.GetRolesAsync();
            return roles.ProjectedAsCollection<RoleDTO>();
        }

        public async Task<RoleDTO> GetRole(int id)
        {
            var role = await _roleRepository.GetRoleAsync(id);
            return role.ProjectedAs<RoleDTO>();
        }

        public async Task<RoleDTO> AddAsync(RoleDTO roleDTO)
        {
            var role = new Role(roleDTO.Name, roleDTO.MenuIds);
            _roleRepository.Add(role);
            await _roleRepository.UnitOfWork.CommitAsync();
            return role.ProjectedAs<RoleDTO>();
        }

        public async Task<RoleDTO> ModifyAsync(RoleDTO roleDTO)
        {
            var role = await _roleRepository.GetRoleAsync(roleDTO.Id);
            role.Name = roleDTO.Name;
            await _roleRepository.RemoveRolMenus(role);

            role.SetNewRoleMenus(roleDTO.MenuIds);
            await _roleRepository.UnitOfWork.CommitAsync();
            return roleDTO;
        }

        public async Task Delete(int id)
        {
            var role = await _roleRepository.GetRoleAsync(id);
            await _roleRepository.RemoveRolMenus(role);
            _roleRepository.Remove(role);
            await _roleRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _roleRepository.Dispose();
        }
        #endregion
    }
}
