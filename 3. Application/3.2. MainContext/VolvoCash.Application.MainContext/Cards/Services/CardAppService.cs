﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Movements;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.Application.MainContext.Cards.Services
{
    public class CardAppService : ICardAppService
    {

        #region Members
        private readonly IContactRepository _contactRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IBatchMovementRepository _batchMovementRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public CardAppService(IContactRepository contactRepository,
            IMovementRepository movementRepository,
            IBatchMovementRepository batchMovementRepository,
                              ICardRepository cardRepository)
        {
            _contactRepository = contactRepository;
            _movementRepository = movementRepository;
            _batchMovementRepository = batchMovementRepository;
            _cardRepository = cardRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<List<CardListDTO>> GetCardsByPhone(string phone)
        {
            var contactChildrenCards = (await _cardRepository.FilterAsync(c => c.Contact.ContactParent.Phone == phone)).ToList();
            var contact = (await _contactRepository.FilterAsync(filter: c => c.Phone == phone, includeProperties: "Cards.CardType")).FirstOrDefault();
            contactChildrenCards.AddRange(contact.Cards);
            if (contactChildrenCards != null && contactChildrenCards.Any())
            {
                var cardsDTO = contactChildrenCards.ProjectedAsCollection<CardListDTO>();
                return cardsDTO;
            }
            return new List<CardListDTO>();
        }

        public async Task<CardDTO> GetCardByPhone(string phone, int id)
        {
            var cards = await _cardRepository.FilterAsync(filter: c => c.Contact.Phone == phone && c.Id == id, includeProperties: "CardType,CardBatches.Batch,Contact");
            if (cards != null && cards.Any())
            {
                var card = cards.FirstOrDefault();
                var movements = await _movementRepository.GetMovementsByCard(id);
                var cardDTO = card.ProjectedAs<CardDTO>();
                cardDTO.Movements = movements.ProjectedAsCollection<MovementDTO>();
                return cardDTO;
            }
            else
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CardNotFound));
            }
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<CardListDTO>> GetCardsByFilter(string query)
        {
            query?.Trim().ToUpper();
            var cards = await _cardRepository.FilterAsync(
                filter: c => c.Code.Trim().ToUpper().Contains(query)
                  || c.Contact.FirstName.ToUpper().Contains(query)
                  || c.Contact.LastName.ToUpper().Contains(query)
                  || c.Contact.Phone.Trim().Contains(query)
                  || c.Contact.Client.Ruc.Trim().Contains(query),
                includeProperties: "Contact.Client,CardType");

            if (cards != null && cards.Any())
            {
                return cards.ProjectedAsCollection<CardListDTO>();
            }
            return new List<CardListDTO>();
        }

        public async Task<List<CardListDTO>> GetCardsByClientId(int? clientId, int? contactId)
        {
            var cards = await _cardRepository.FilterAsync(
                filter: c => (  clientId == null ||  c.Contact.ClientId == clientId) &&
                              (contactId == null ||  c.Contact.Id == contactId) ,
                includeProperties: "Contact,CardType");

            if (cards != null && cards.Any())
            {
                return cards.ProjectedAsCollection<CardListDTO>();
            }
            return new List<CardListDTO>();
        }

        public async Task<List<CardListDTO>> GetCardsByClientIdAndCardTypeId(int clientId, int cardTypeId)
        {
            var cards = await _cardRepository.FilterAsync(
                filter: c => c.Contact.ClientId == clientId && c.CardTypeId == cardTypeId,
                includeProperties: "Contact,CardType");

            if (cards != null && cards.Any())
            {
                return cards.ProjectedAsCollection<CardListDTO>();
            }
            return new List<CardListDTO>();
        }

        public async Task<List<BatchMovementDTO>> GetCardBatchMovements(int cardId, int batchId)
        {
            var batchMovements = await _batchMovementRepository.FilterAsync(
                filter: bm => bm.Movement.CardId == cardId && bm.BatchId == batchId,
                includeProperties: "Movement.Charge.Cashier.Dealer,Movement.Transfer,Movement.Card");

            if (batchMovements != null && batchMovements.Any())
            {
                return batchMovements.ProjectedAsCollection<BatchMovementDTO>();
            }
            return new List<BatchMovementDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _cardRepository.Dispose();
            _contactRepository.Dispose();
            _cardRepository.Dispose();
        }
        #endregion
    }
}
