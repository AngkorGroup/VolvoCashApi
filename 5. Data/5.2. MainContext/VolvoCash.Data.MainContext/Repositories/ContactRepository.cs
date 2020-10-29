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
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Phone == phone);
            return contact;
        }
        public async Task<Contact> CreateOrUpdateMainContact(Client client, string phone, DocumentType documentType,
            string documentNumber, string firstName, string lastName, string email)
        {
            var mainContact = client.Contacts.FirstOrDefault(c => c.Type == ContactType.Primary);
            var existingContactInOtherClient = (await FilterAsync(c => c.ClientId != client.Id && c.Phone == phone)).FirstOrDefault();
            if (mainContact == null)
            {
                if (existingContactInOtherClient != null)
                {
                    throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Infraestructure.exception_ContactAlreadyExistsForOtherClient));
                }
                mainContact = new Contact(
                    client,
                    ContactType.Primary,
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
                        var existingContact = client.Contacts.Where(c => c.Phone == phone).FirstOrDefault();
                        if (existingContact == null)
                        {
                            var newContact = new Contact(
                                client,
                                ContactType.Secondary,
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
        #endregion
    }
}
