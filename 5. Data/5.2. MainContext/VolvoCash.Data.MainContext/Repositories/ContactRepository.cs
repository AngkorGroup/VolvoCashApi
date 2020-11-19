﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.EnumAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class ContactRepository : Repository<Contact, MainDbContext>, IContactRepository
    {
        private readonly ILocalization _resources;
        #region Constructor
        public ContactRepository(MainDbContext dbContext,
                                 ILogger<Repository<Contact, MainDbContext>> logger) : base(dbContext, logger)
        {
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Public Methods
        public async Task<Contact> GetByPhoneAsync(string phone)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Phone == phone && c.Status.Active == 1);
            return contact;
        }

        public async Task<Contact> CreateOrUpdateMainContact(Client client, string phone, DocumentType documentType,
            string documentNumber, string firstName, string lastName, string email)
        {
            var mainContact = client.Contacts.FirstOrDefault(c => c.Type == new ContactType("Primary","###") && c.Status.Active == 1);
            var existingContactInOtherClient = (await FilterAsync(c => c.ClientId != client.Id && c.Phone == phone && c.Status.Active == 1)).FirstOrDefault();
            if (mainContact == null)
            {
                if (existingContactInOtherClient != null)
                {
                    throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Infraestructure.exception_ContactAlreadyExistsForOtherClient));
                }
                var contactType = new ContactType("Primary", "###");
                mainContact = new Contact(
                    client,
                    contactType,
                    documentType,
                    documentNumber,
                    phone,
                    firstName,
                    lastName,
                    email
                );
            }
            else
            {
                if (mainContact.Phone == phone)
                {
                    mainContact.DocumentType = documentType;
                    mainContact.DocumentNumber = documentNumber;
                    mainContact.Email = email;
                    mainContact.FirstName = firstName;
                    mainContact.LastName = lastName;
                }
                else
                {
                    if (existingContactInOtherClient == null)
                    {
                        var existingContact = client.Contacts.Where(c => c.Phone == phone && c.Status.Active == 1).FirstOrDefault();
                        if (existingContact == null)
                        {
                            var contactType = new ContactType("Secondary", "###");
                            var newContact = new Contact(
                                client,
                                contactType,
                                documentType,
                                documentNumber,
                                phone,
                                firstName,
                                lastName,
                                email
                            );
                            client.Contacts.Add(newContact);
                        }
                        else
                        {
                            existingContact.DocumentType = documentType;
                            existingContact.DocumentNumber = documentNumber;
                            existingContact.Email = email;
                            existingContact.FirstName = firstName;
                            existingContact.LastName = lastName;
                        }
                    }
                }
            }
            return mainContact;
        }

        public async Task<Contact> GetContactById(int id)
        {
            return (await FilterAsync(filter: (c) => c.Id == id)).FirstOrDefault();
        }

        public async Task<Contact> GetContactByIdWithCards(int id)
        {
            return (await FilterAsync(filter: (c) => c.Id == id, includeProperties: "Cards")).FirstOrDefault();
        }

        public async Task<Contact> GetPrimaryByClientId(int clientId)
        {
            return (await FilterAsync(filter: (c) => c.ClientId == clientId && c.Type.Name == "Primary")).FirstOrDefault();
        }
        #endregion
    }
}
