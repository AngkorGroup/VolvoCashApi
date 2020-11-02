using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.Clients.Services
{
    public interface IClientAppService : IDisposable
    {
        #region ApiWeb
        Task<List<ClientFilterDTO>> GetClientsByFilter(string query,int maxRecords);
        Task<List<ClientListDTO>> GetClientsByPagination(string query, int pageIndex, int pageLength);
        Task<List<CardTypeSummary>> GetClientCardTypesSummary(int id);
        #endregion
    }
}
