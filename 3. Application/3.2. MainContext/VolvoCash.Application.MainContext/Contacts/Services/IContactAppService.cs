using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Contacts;

namespace VolvoCash.Application.MainContext.Contacts.Services
{
    public interface IContactAppService : IDisposable
    {
        #region ApiClient
        Task<List<ContactListDTO>> GetContactsByPhone(string phone);
        Task<ContactListDTO> AddContact(ContactDTO contactDTO);
        #endregion

        #region ApiPOS
        Task<List<ContactListDTO>> GetContacts(string query, int pageIndex, int pageLength);
        Task<List<CardListDTO>> GetContactCards(int id);
        #endregion

        #region ApiWeb
        Task<List<ContactListDTO>> GetContactsByClientId(int clientId);
        #endregion
    }
}
