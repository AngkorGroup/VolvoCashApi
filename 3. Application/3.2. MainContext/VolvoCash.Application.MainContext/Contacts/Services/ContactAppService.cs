using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.EnumAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Contacts.Services
{
    public class ContactAppService : IContactAppService
    {
        #region Members
        private readonly IContactRepository _contactRepository;
        private readonly ISMSManager _smsManager;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public ContactAppService(IContactRepository contactRepository,
                                 ISMSManager smsManager)
        {
            _contactRepository = contactRepository;
            _smsManager = smsManager;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Private Methods
        private void SendWelcomeMessageToContact(ContactDTO contactDTO)
        {
            var body = _resources.GetStringResource(LocalizationKeys.Application.messages_NewContactMessage);
            _smsManager.SendSMS(contactDTO.Phone, body);            
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<List<ContactListDTO>> GetContactsByPhone(string phone)
        {
            var currentContact = (await _contactRepository.FilterAsync(
                filter: c => c.Phone == phone && c.Status == Status.Active
            )).FirstOrDefault();

            var contacts = await _contactRepository.FilterAsync( filter: c => c.ClientId == currentContact.ClientId &&  c.Id != currentContact.Id && c.Status == Status.Active );

            if (contacts != null && contacts.Any())
            {
                return contacts.ProjectedAsCollection<ContactListDTO>();
            }
            return new List<ContactListDTO>();
        }

        public async Task<ContactListDTO> AddContact(ContactDTO contactDTO)
        {
            var currentContact = (await _contactRepository.FilterAsync(filter: c => c.Phone == contactDTO.ContactParent.Phone, includeProperties: "Client")).FirstOrDefault();
            var existingContact = _contactRepository.Filter(c => c.Phone == contactDTO.Phone && c.Status == Status.Active).FirstOrDefault();
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
                SendWelcomeMessageToContact(contactDTO);                
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
            query = query.Trim().ToUpper();
            var contacts = await _contactRepository.GetFilteredAsync(
                c => (c.FirstName.ToUpper().Contains(query)
                  || c.LastName.ToUpper().Contains(query)
                  || c.Phone.Contains(query)
                  || c.DocumentNumber.Contains(query)) && c.Status == Status.Active,
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
            var cards = (await _contactRepository.FilterAsync(c => c.Id == id && c.Status == Status.Active, includeProperties: "Cards.CardType,Cards.CardBatches.Batch")).FirstOrDefault().Cards;

            if (cards != null && cards.Any())
            {
                return cards.ProjectedAsCollection<CardListDTO>();
            }
            return new List<CardListDTO>();
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<ContactListDTO>> GetContactsByClientId(int clientId, bool onlyActive)
        {
            var contacts = await _contactRepository.FilterAsync(filter: c => c.ClientId == clientId && (!onlyActive || c.Status == Status.Active));

            if (contacts != null && contacts.Any())
            {
                return contacts.ProjectedAsCollection<ContactListDTO>();
            }
            return new List<ContactListDTO>();
        }

        public async Task<List<ContactListDTO>> GetContactsByFilter(string query,int maxRecords,bool onlyActive)
        {
            query = query.Trim().ToUpper();
            var contacts = await _contactRepository.FilterAsync(
                filter: c => (c.FirstName.ToUpper().Contains(query)
                || c.LastName.ToUpper().Contains(query)
                || c.Phone.Contains(query)
                || c.DocumentNumber.Contains(query)) && (!onlyActive || c.Status == Status.Active),
                includeProperties:"Client");

            contacts = contacts.Take(Math.Min(contacts.Count(), maxRecords));
            if (contacts != null && contacts.Any())
            {
                return contacts.ProjectedAsCollection<ContactListDTO>();
            }
            return new List<ContactListDTO>();
        }

        public async Task<ContactDTO> UpdateContact(ContactDTO contactDTO)
        {
            if (contactDTO == null)
            {
                throw new ArgumentException(_resources.GetStringResource(LocalizationKeys.Application.exception_CannotUpdateContactWithEmptyInformation));
            }

            var contactPersisted = await _contactRepository.GetAsync(contactDTO.Id);
            if (contactPersisted != null)
            {
                contactPersisted.FirstName = contactDTO.FirstName;
                contactPersisted.LastName = contactDTO.LastName;
                contactPersisted.Phone = contactDTO.Phone;
                contactPersisted.Email = contactDTO.Email;
                contactPersisted.DocumentType = contactDTO.DocumentType;
                contactPersisted.DocumentNumber = contactDTO.DocumentNumber;
                contactPersisted.Status = contactDTO.Status;
                
                _contactRepository.Modify(contactPersisted);
                await _contactRepository.UnitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(_resources.GetStringResource(LocalizationKeys.Application.exception_CannotUpdateNonExistingContact));
            }
            return contactPersisted.ProjectedAs<ContactDTO>();
        }

        public async Task MakeContactAsPrimary(int id)
        {
            var contactToBePrimary = await _contactRepository.GetAsync(id);
            var currentPrimary = await _contactRepository.GetPrimaryByClientId(contactToBePrimary.ClientId);

            if (currentPrimary != null){
                currentPrimary.Type = ContactType.Secondary;
            }
            contactToBePrimary.Type = ContactType.Primary;

            await _contactRepository.UnitOfWork.CommitAsync();
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
