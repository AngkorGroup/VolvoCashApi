using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;

namespace VolvoCash.Application.MainContext.Clients.Services
{
    public class ClientAppService : IClientAppService
    {
        #region Members
        private IClientRepository _clientRepository;
        private readonly ILogger _logger;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public ClientAppService(IClientRepository clientRepository,
                                ILogger<ClientAppService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<ClientListDTO>> GetClients(string query, int pageIndex, int pageLength)
        {
            var clients = await _clientRepository.GetFilteredAsync(
                c => c.Name.ToUpper().Contains(query.Trim().ToUpper())
                  || c.Ruc.Contains(query.Trim())
                  || c.Address.ToUpper().Contains(query.Trim().ToUpper())
                  || c.Phone.Contains(query.Trim()),
                pageIndex,
                pageLength,
                c => c.Name,
                true);

            if (clients != null && clients.Any())
            {
                return clients.ProjectedAsCollection<ClientListDTO>();
            }
            return new List<ClientListDTO>();
        }

        public async Task<List<CardTypeSummary>> GetClientCardTypesSummary(int id)
        {
            var client = (await _clientRepository.FilterAsync(
                filter: c => c.Id == id,
                includeProperties: "Contacts.Cards"
            )).FirstOrDefault();

            if (client != null)
            {
                return client.GetCardTypesSummary();
            }
            return new List<CardTypeSummary>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            //dispose all resources
            _clientRepository.Dispose();
        }
        #endregion
    }
}
