using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;

namespace VolvoCash.Application.MainContext.Clients.Services
{
    public class ClientAppService : IClientAppService
    {
        #region Members
        private readonly IClientRepository _clientRepository;
        #endregion

        #region Constructor
        public ClientAppService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<ClientFilterDTO>> GetClientsByFilter(string query, int maxRecords)
        {
            query = query?.Trim().ToUpper();
            var clients = await _clientRepository.FilterAsync(
                filter: c => c.Name.Trim().ToUpper().Contains(query)
                          || c.Ruc.Trim().Contains(query)
                          || c.Address.Trim().ToUpper().Contains(query)
                          || c.Phone.Trim().Contains(query)
                          || string.IsNullOrEmpty(query),
                includeProperties : "Contacts.Cards.Currency");
            clients = clients.Take(Math.Min(clients.Count(), maxRecords));
            if (clients != null && clients.Any())
            {
                return clients.ProjectedAsCollection<ClientFilterDTO>();
            }
            return new List<ClientFilterDTO>();
        }

        public async Task<List<ClientListDTO>> GetClientsByPagination(string query, int pageIndex, int pageLength)
        {
            query = query?.Trim().ToUpper();
            var clients = await _clientRepository.GetFilteredAsync(
                c => c.Name.Trim().ToUpper().Contains(query)
                  || c.Ruc.Trim().Contains(query)
                  || c.Address.Trim().ToUpper().Contains(query)
                  || c.Phone.Trim().Contains(query)
                  || string.IsNullOrEmpty(query),
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
