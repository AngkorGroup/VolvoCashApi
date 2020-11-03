using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
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
        public async Task<List<ClientFilterDTO>> GetClientsByFilter(string query, int maxRecords)
        {
            query.Trim().ToUpper();
            var clients = await _clientRepository.FilterAsync(
                filter: c => c.Name.Trim().ToUpper().Contains(query)
                          || c.Ruc.Trim().Contains(query)
                          || c.Address.Trim().ToUpper().Contains(query)
                          || c.Phone.Trim().Contains(query),
                includeProperties : "Contacts.Cards"
            );
            clients = clients.Take(Math.Min(clients.Count(), maxRecords));
            if (clients != null && clients.Any())
            {
                return clients.ProjectedAsCollection<ClientFilterDTO>();
            }
            return new List<ClientFilterDTO>();
        }

        public async Task<List<ClientListDTO>> GetClientsByPagination(string query, int pageIndex, int pageLength)
        {
            query?.Trim().ToUpper();
            var clients = await _clientRepository.GetFilteredAsync(
                c => c.Name.Trim().ToUpper().Contains(query)
                  || c.Ruc.Trim().Contains(query)
                  || c.Address.Trim().ToUpper().Contains(query)
                  || c.Phone.Trim().Contains(query),
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

        public async Task<List<CardTypeSummaryDTO>> GetClientCardTypesSummary(int id)
        {
            var client = (await _clientRepository.FilterAsync(
                filter: c => c.Id == id,
                includeProperties: "Contacts.Cards.CardType"
            )).FirstOrDefault();

            if (client != null)
            {
                var summary = client.GetCardTypesSummary();
                return summary.ProjectedAsCollection<CardTypeSummaryDTO>();
            }
            return new List<CardTypeSummaryDTO>();
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
