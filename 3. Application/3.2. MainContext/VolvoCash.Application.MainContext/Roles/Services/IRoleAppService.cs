using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Roles;

namespace VolvoCash.Application.MainContext.Roles.Services
{
    public interface IRoleAppService : IDisposable
    {
        #region ApiWeb
        Task<List<RoleDTO>> GetRoles();
        Task<RoleDTO> GetRole(int id);
        Task<RoleDTO> AddAsync(RoleDTO roleDTO);
        Task<RoleDTO> ModifyAsync(RoleDTO roleDTO);
        Task Delete(int id);
        #endregion
    }
}
