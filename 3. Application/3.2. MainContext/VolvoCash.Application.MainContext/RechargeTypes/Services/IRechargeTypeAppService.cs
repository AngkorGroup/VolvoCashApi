using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.RechargeTypes;

namespace VolvoCash.Application.MainContext.RechargeTypes.Services
{
    public interface IRechargeTypeAppService : IDisposable
    {
        Task<List<RechargeTypeDTO>> GetRechargeTypes(bool onlyActive);

        Task<RechargeTypeDTO> GetRechargeType(int id);

        Task<RechargeTypeDTO> AddAsync(RechargeTypeDTO rechargeTypeDTO);

        Task<RechargeTypeDTO> ModifyAsync(RechargeTypeDTO rechargeTypeDTO);

        Task Delete(int id);
    }
}
