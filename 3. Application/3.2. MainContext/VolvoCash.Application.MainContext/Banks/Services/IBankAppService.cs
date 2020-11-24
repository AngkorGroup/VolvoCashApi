using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Banks;

namespace VolvoCash.Application.MainContext.Banks.Services
{
    public interface IBankAppService : IDisposable
    {
        #region ApiWeb
        Task<List<BankDTO>> GetBanks(bool onlyActive);
        Task<BankDTO> GetBank(int id);
        Task<BankDTO> AddAsync(BankDTO bankDTO);
        Task<BankDTO> ModifyAsync(BankDTO bankDTO);
        Task Delete(int id);
        #endregion
    }
}
