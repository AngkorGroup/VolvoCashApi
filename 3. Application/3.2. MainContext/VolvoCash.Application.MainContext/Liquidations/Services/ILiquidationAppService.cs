using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Liquidations.Services
{
    public interface ILiquidationAppService : IDisposable
    {
        #region ApiWeb
        Task<List<LiquidationDTO>> GetLiquidations(DateTime date, LiquidationStatus liquidationStatus);
        Task<LiquidationDTO> GetLiquidation(int id);
        // Task<LiquidationDTO> AddAsync(LiquidationDTO liquidationDTO);
        #endregion
    }
}
