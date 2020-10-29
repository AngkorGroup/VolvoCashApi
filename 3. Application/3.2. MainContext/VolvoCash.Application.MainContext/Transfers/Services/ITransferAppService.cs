using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Transfers;

namespace VolvoCash.Application.MainContext.Transfers.Services
{
    public interface ITransferAppService : IDisposable
    {
        #region ApiClient
        Task<List<TransferListDTO>> GetTransfersByPhone(string phone);
        Task<TransferListDTO> PerformTransfer(string phone, TransferDTO transferDTO);
        Task<TransferDTO> GetTransferById(int id);
        #endregion
    }
}
