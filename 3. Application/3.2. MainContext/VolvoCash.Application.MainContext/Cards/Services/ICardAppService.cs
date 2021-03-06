﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Common;

namespace VolvoCash.Application.MainContext.Cards.Services
{
    public interface ICardAppService : IDisposable
    {
        #region ApiClient
        Task<List<CardListDTO>> GetCardsByPhone(string phone, int? contactId);
        Task<MoneyDTO> GetTotalBalance(string phone, int? contactId);
        Task<CardDTO> GetCardByPhone(string phone, int id);
        #endregion

        #region ApiWeb
        Task<List<CardListDTO>> GetCardsByFilter(string query, int maxRecords);
        Task<List<CardListDTO>> GetCardsByClientId(int? clientId, int? contactId);
        Task<List<CardListDTO>> GetCardsByClientIdAndCardTypeId(int clientId, int cardTypeId);
        Task<List<BatchMovementDTO>> GetCardBatchMovements(int cardId, int batchId);
        #endregion
    }
}
