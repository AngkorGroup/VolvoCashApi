using System.Threading.Tasks;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.ContactAgg
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetByPhoneAsync(string phone);
        Task<Contact> CreateOrUpdateMainContact(Client client, string phone, DocumentType documentType, string documentNumber, string firstName, string lastName, string email);
        Task<Contact> GetContactById(int id);
        Task<Contact> GetContactByIdWithCards(int id);
        Task<Contact> GetPrimaryByClientId(int clientId);
    }
}
