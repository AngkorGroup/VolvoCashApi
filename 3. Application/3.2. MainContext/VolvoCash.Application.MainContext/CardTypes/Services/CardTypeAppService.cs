using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.CardTypes.Services
{
    public class CardTypeAppService : ICardTypeAppService
    {
        #region Members
        private readonly ICardTypeRepository _cardTypeRepository;
        #endregion

        #region Constructor
        public CardTypeAppService(ICardTypeRepository cardTypeRepository)
        {
            _cardTypeRepository = cardTypeRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<CardTypeDTO>> GetCardTypes(bool onlyActive)
        {
            var cardTypes = await _cardTypeRepository.FilterAsync(filter: c => !onlyActive || c.Status == Status.Active
                                                                          , includeProperties: "Currency");
            return cardTypes.ProjectedAsCollection<CardTypeDTO>();
        }

        public async Task<CardTypeDTO> AddAsync(CardTypeDTO cardTypeDTO)
        {
            var cardType = new CardType(cardTypeDTO.Name, cardTypeDTO.DisplayName,cardTypeDTO.Term,cardTypeDTO.CurrencyId,cardTypeDTO.Color,cardTypeDTO.TPCode);
            _cardTypeRepository.Add(cardType);
            await _cardTypeRepository.UnitOfWork.CommitAsync();
            return cardType.ProjectedAs<CardTypeDTO>();
        }

        public async Task<CardTypeDTO> ModifyAsync(CardTypeDTO cardTypeDTO)
        {
            var cardType = await _cardTypeRepository.GetAsync(cardTypeDTO.Id);

            cardType.Name = cardTypeDTO.Name;
            cardType.DisplayName = cardTypeDTO.DisplayName;
            cardType.Term = cardTypeDTO.Term;
            cardType.CurrencyId = cardTypeDTO.CurrencyId;
            cardType.Color = cardTypeDTO.Color;
            cardType.TPCode = cardTypeDTO.TPCode;

            _cardTypeRepository.Modify(cardType);
            await _cardTypeRepository.UnitOfWork.CommitAsync();
            return cardType.ProjectedAs<CardTypeDTO>();
        }

        public async Task Delete(int id)
        {
            var cardType = await _cardTypeRepository.GetAsync(id);
            cardType.ArchiveAt = DateTime.Now;
            cardType.Status = Status.Inactive;
            _cardTypeRepository.Modify(cardType);
            await _cardTypeRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _cardTypeRepository.Dispose();
        }
        #endregion
    }
}
