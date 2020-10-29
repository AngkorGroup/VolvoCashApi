using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Clients;

namespace VolvoCash.Application.MainContext.Clients.Services
{
    public interface IClientAppService : IDisposable
    {
        #region ApiWeb
        Task<List<ClientListDTO>> GetClients(string query, int pageIndex, int pageLength);
        #endregion
    }
}
