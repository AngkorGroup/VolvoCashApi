using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Contacts.Services
{
    public class ContactAppService : IContactAppService
    {
        #region Members
        private IContactRepository _contactRepository;
        private readonly ILogger _logger;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public ContactAppService(IContactRepository contactRepository,
                                 ILogger<ContactAppService> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<List<ContactListDTO>> GetContactsByPhone(string phone)
        {
            var currentContact = (await _contactRepository.FilterAsync(
                filter: c => c.Phone == phone
            )).FirstOrDefault();

            var contacts = await _contactRepository.FilterAsync(
                filter: (c => c.ClientId == currentContact.ClientId &&
                              c.Id != currentContact.Id)
            );

            if (contacts != null && contacts.Any())
            {
                return contacts.ProjectedAsCollection<ContactListDTO>();
            }
            return new List<ContactListDTO>();
        }

        public async Task<ContactListDTO> AddContact(ContactDTO contactDTO)
        {
            var currentContact = (await _contactRepository.FilterAsync(filter: c => c.Phone == contactDTO.ContactParent.Phone, includeProperties: "Client")).FirstOrDefault();
            var existingContact = _contactRepository.Filter(c => c.Phone == contactDTO.Phone).FirstOrDefault();
            if (existingContact != null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_ContactAlreadyExists));
            }
            if (currentContact.Client != null)
            {
                var contact = new Contact(
                    currentContact.Client,
                    ContactType.Secondary,
                    contactDTO.DocumentType,
                    contactDTO.DocumentNumber,
                    contactDTO.Phone,
                    contactDTO.FirstName,
                    contactDTO.LastName,
                    contactDTO.Email,
                    currentContact.Id
                );
                _contactRepository.Add(contact);
                await _contactRepository.UnitOfWork.CommitAsync();
                return contact.ProjectedAs<ContactListDTO>();
            }
            else
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CannotCreateContactForNonExistingClient));
            }
        }
        #endregion

        #region ApiPOS Public Methods
        public async Task<List<ContactListDTO>> GetContacts(string query, int pageIndex, int pageLength)
        {
            var contacts = await _contactRepository.GetFilteredAsync(
                c => c.FirstName.ToUpper().Contains(query.Trim().ToUpper())
                  || c.LastName.ToUpper().Contains(query.Trim().ToUpper())
                  || c.Phone.Contains(query.Trim())
                  || c.DocumentNumber.Contains(query.Trim()),
                pageIndex,
                pageLength,
                c => c.LastName,
                true);

            if (contacts != null && contacts.Any())
            {
                return contacts.ProjectedAsCollection<ContactListDTO>();
            }
            return new List<ContactListDTO>();
        }

        public async Task<List<CardListDTO>> GetContactCards(int id)
        {
            var cards = (await _contactRepository.FilterAsync(c => c.Id == id, includeProperties: "Cards.CardType,Cards.CardBatches.Batch")).FirstOrDefault().Cards;

            if (cards != null && cards.Any())
            {
                return cards.ProjectedAsCollection<CardListDTO>();
            }
            return new List<CardListDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            //dispose all resources
            _contactRepository.Dispose();
        }
        #endregion
    }
}
