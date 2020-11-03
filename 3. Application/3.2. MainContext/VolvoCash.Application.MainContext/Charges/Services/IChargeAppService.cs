using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Charges.Services
{
    public interface IChargeAppService : IDisposable
    {
        #region ApiClient
        Task<List<ChargeDTO>> GetChargesByPhone(string phone);
        Task<ChargeDTO> GetChargeByPhone(string phone, int id);
        Task<ChargeDTO> PerformChargeByPhone(string phone, int chargeId, bool confirmed);
        #endregion

        #region ApiPOS
        Task<List<ChargeDTO>> GetChargesByCashierId(int id, ChargeType chargeType, int pageIndex, int pageLength);
        Task<ChargeDTO> AddChargeRemote(ChargeDTO chargeDTO);
        Task<ChargeDTO> AddChargeFaceToFace(ChargeDTO chargeDTO);
        Task<ChargeDTO> GetChargeById(int id);
        #endregion
    }
}
