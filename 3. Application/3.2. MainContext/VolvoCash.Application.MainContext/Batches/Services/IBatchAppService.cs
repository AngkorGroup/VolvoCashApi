using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.CardBatches;

namespace VolvoCash.Application.MainContext.Batches.Services
{
    public interface IBatchAppService : IDisposable
    {
        #region ApiWeb 
        Task<List<BatchDTO>> GetBatches();
        Task<List<CardBatchDTO>> GetBatchesByCardId(int cardId);
        Task<List<CardBatchDTO>> GetBatchesByClientId(int clientId);
        Task<List<BatchErrorDTO>> GetErrorBatches();
        Task<List<BatchErrorDTO>> PerformLoadsFromFileStreamAsync(string fileName, Stream stream);
        Task<BatchDTO> PerformLoadAsync(ClientDTO clientDTO, ContactDTO contactDTO, CardDTO carDTO, BatchDTO batchDTO);
        #endregion
    }
}
