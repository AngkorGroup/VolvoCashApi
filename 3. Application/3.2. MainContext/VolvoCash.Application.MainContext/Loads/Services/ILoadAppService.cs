using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Contacts;

namespace VolvoCash.Application.MainContext.Loads.Services
{
    public interface ILoadAppService : IDisposable
    {
        #region ApiWeb 
        Task<List<BatchDTO>> GetLoads();
        Task<List<BatchErrorDTO>> GetErrorLoads();
        Task<List<BatchErrorDTO>> PerformLoadsFromFileStreamAsync(string fileName, Stream stream);
        Task<BatchDTO> PerformLoadAsync(ClientDTO clientDTO, ContactDTO contactDTO, CardDTO carDTO, BatchDTO batchDTO);
        #endregion
    }
}
