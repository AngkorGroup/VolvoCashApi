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
        Task<List<ClientListDTO>> GetClients(string query, int pageIndex, int pageLength);
        Task<List<CardTypeSummary>> GetClientCardTypesSummary(int id);
        #endregion
    }
}
