using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;

namespace VolvoCash.Application.MainContext.Cards.Services
{
    public class CardAppService : ICardAppService
    {

        #region Members
        private readonly IContactRepository _contactRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public CardAppService(IContactRepository contactRepository,
                              ICardRepository cardRepository)
        {
            _contactRepository = contactRepository;
            _cardRepository = cardRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<List<CardListDTO>> GetCardsByPhone(string phone)
        {
            var cards = new List<Card>();
            var contact = (await _contactRepository.FilterAsync(filter: c => c.Phone == phone,
                                                                includeProperties: "Cards.CardType,Cards.CardBatches.Batch,ContactChildren.Cards.CardType")
            ).FirstOrDefault();
            cards.AddRange(contact.Cards);
            contact.ContactChildren.ToList().ForEach(c => cards.AddRange(c.Cards));
            if (cards != null && cards.Any())
            {
                var cardsDTO = cards.ProjectedAsCollection<CardListDTO>();
                return cardsDTO;
            }
            return new List<CardListDTO>();
        }

        public async Task<CardDTO> GetCardByPhone(string phone, int id)
        {
            var cards = await _cardRepository.FilterAsync(filter: c => c.Id == id && c.Contact.Phone == phone,
                                                          includeProperties: "CardType,Movements.Charge,Movements.Transfer,CardBatches.Batch,Contact");
            if (cards != null && cards.Any())
            {
                var card = cards.FirstOrDefault();
                var cardDTO = card.ProjectedAs<CardDTO>();
                return cardDTO;
            }
            else
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CardNotFound));
            }
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
