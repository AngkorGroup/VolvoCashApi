using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.CardBatches;
using VolvoCash.Application.MainContext.DTO.POJOS;

namespace VolvoCash.Application.MainContext.Batches.Services
{
    public interface IBatchAppService : IDisposable
    {
        #region ApiWeb 
        Task<List<BatchDTO>> GetBatches(DateTime? beginDate, DateTime? endDate);
        Task<List<BatchDTO>> GetBatchesByExpiresAtExtent(string clientId, DateTime? beginDate, DateTime? endDate);
        Task<List<CardBatchDTO>> GetBatchesByCardId(int cardId);
        Task<List<CardBatchDTO>> GetBatchesByClientId(int clientId);
        Task<List<BatchErrorDTO>> GetErrorBatches();
        List<Load> GetLoadsFromFileStream(string fileName, Stream stream);
        Task<List<BatchErrorDTO>> PerformLoadsFromFileStreamAsync(string fileName, Stream stream);
        Task ExtendExpiredDate(int id, DateTime newExpiredDate);
        #endregion

        #region ConsoleApp 
        Task PerformBatchExpiration();
        #endregion
    }
}
