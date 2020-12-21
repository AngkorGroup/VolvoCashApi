using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Clients;

namespace VolvoCash.Application.MainContext.Clients.Services
{
    public interface IClientAppService : IDisposable
    {
        #region ApiClient
        Task<List<ClientListDTO>> GetClientsByPhone(string phone);
        #endregion

        #region ApiWeb
        Task<List<ClientFilterDTO>> GetClientsByFilter(string query, int maxRecords);
        Task<List<ClientListDTO>> GetClientsByPagination(string query, int pageIndex, int pageLength);
        Task<List<CardTypeSummaryDTO>> GetClientCardTypesSummary(int id);
        #endregion
    }
}
