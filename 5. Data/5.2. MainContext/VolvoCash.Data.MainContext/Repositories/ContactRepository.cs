using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Enums;

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
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Phone == phone && c.Status == Status.Active);
            return contact;
        }

        public async Task<Contact> CreateOrUpdateMainContact(Client client, string phone, int documentTypeId,
            string documentNumber, string firstName, string lastName, string email)
        {
            var mainContact = client.Contacts.FirstOrDefault(c => c.Type == ContactType.Primary && c.Status == Status.Active);
            //var existingContactInOtherClient = (await FilterAsync(c => c.ClientId != client.Id && c.Phone == phone && c.Status == Status.Active)).FirstOrDefault();
            if (mainContact == null)
            {
                mainContact = new Contact(
                    client,
                    ContactType.Primary,
                    documentTypeId,
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
                    mainContact.DocumentTypeId = documentTypeId;
                    mainContact.DocumentNumber = documentNumber;
                    mainContact.Email = email;
                    mainContact.FirstName = firstName;
                    mainContact.LastName = lastName;
                }
                else
                {
                    var existingContact = client.Contacts.Where(c => c.Phone == phone && c.Status == Status.Active).FirstOrDefault();
                    if (existingContact == null)
                    {
                        var newContact = new Contact(
                            client,
                            ContactType.Secondary,
                            documentTypeId,
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
                        existingContact.DocumentTypeId = documentTypeId;
                        existingContact.DocumentNumber = documentNumber;
                        existingContact.Email = email;
                        existingContact.FirstName = firstName;
                        existingContact.LastName = lastName;
                    }
                    var allOtherContacts = await FilterAsync(c => c.Phone == phone && c.Status == Status.Active && c.ClientId != client.Id);
                    foreach(var contact in allOtherContacts)
                    {
                        contact.DocumentTypeId = documentTypeId;
                        contact.DocumentNumber = documentNumber;
                        contact.Email = email;
                        contact.FirstName = firstName;
                        contact.LastName = lastName;
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
            return (await FilterAsync(filter: (c) => c.ClientId == clientId && c.Type == ContactType.Primary)).FirstOrDefault();
        }
        #endregion
    }
}
