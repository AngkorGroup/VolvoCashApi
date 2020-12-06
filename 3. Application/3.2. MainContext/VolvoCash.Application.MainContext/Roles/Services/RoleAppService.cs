using System.Linq;
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
            var roles = await _roleRepository.FilterAsync(includeProperties: "RoleMenus");
            return roles.ProjectedAsCollection<RoleDTO>();
        }

        public async Task<RoleDTO> GetRole(int id)
        {
            var role = (await _roleRepository.FilterAsync(
                filter: r => r.Id == id,
                includeProperties: "RoleMenus")).FirstOrDefault();
            return role.ProjectedAs<RoleDTO>();
        }

        public async Task<RoleDTO> AddAsync(RoleDTO roleDTO)
        {
            var role = new Role(
                roleDTO.Name,
                roleDTO.MenuIds
            );
            _roleRepository.Add(role);
            await _roleRepository.UnitOfWork.CommitAsync();
            return role.ProjectedAs<RoleDTO>();
        }

        public async Task<RoleDTO> ModifyAsync(RoleDTO roleDTO)
        {
            var role = await _roleRepository.GetAsync(roleDTO.Id);
            role.Name = roleDTO.Name;
            await _roleRepository.UnitOfWork.CommitAsync();
            return roleDTO;
        }

        public async Task Delete(int id)
        {
            var role = await _roleRepository.GetAsync(id);
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
