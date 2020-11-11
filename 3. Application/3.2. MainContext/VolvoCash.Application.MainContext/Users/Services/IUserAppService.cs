using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Application.MainContext.DTO.Users;

namespace VolvoCash.Application.MainContext.Users.Services
{
    public interface IUserAppService : IDisposable
    {
        Task<IList<UserDTO>> GetAllDTOAsync();
        Task<AdminDTO> AddAdminAsync(AdminDTO adminDTO);
        Task<AdminDTO> ModifyAdminAsync(AdminDTO adminDTO);
        Task ResetUserPasswordAsync(int id);
        Task DeleteUserAsync(int id, int? contactId);
    }
}
