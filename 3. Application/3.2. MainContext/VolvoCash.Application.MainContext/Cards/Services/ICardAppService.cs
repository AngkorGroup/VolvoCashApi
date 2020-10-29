using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Cards;

namespace VolvoCash.Application.MainContext.Cards.Services
{
    public interface ICardAppService : IDisposable
    {
        #region ApiClient
        Task<List<CardListDTO>> GetCardsByPhone(string phone);
        Task<CardDTO> GetCardByPhone(string phone, int id);
        #endregion
    }
}
