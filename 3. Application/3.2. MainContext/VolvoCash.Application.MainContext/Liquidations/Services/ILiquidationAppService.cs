using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Liquidations;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Liquidations.Services
{
    public interface ILiquidationAppService : IDisposable
    {
        #region ApiWeb
        Task<List<LiquidationDTO>> GetLiquidations(DateTime beginDate, DateTime endDate, LiquidationStatus liquidationStatus);
        Task<LiquidationDTO> GetLiquidation(int id);
        Task<List<LiquidationDTO>> GenerateLiquidations();
        Task<List<ChargeListDTO>> GetLiquidationCharges(int id);
        Task<byte[]> ScheduleLiquidations(int bankId, int bankAccountId, List<int> liquidationsId);
        Task PayLiquidation(int id, string voucher, DateTime paymentDate);
        Task CancelLiquidation(int id);       
        #endregion
    }
}
