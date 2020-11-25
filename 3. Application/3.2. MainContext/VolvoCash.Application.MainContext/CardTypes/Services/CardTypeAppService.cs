using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.Seedwork;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.CardTypes.Services
{
    public class CardTypeAppService : Service<CardType, CardTypeDTO>, ICardTypeAppService
    {
        #region Members
        #endregion

        #region Constructor
        public CardTypeAppService(ICardTypeRepository cardTypeRepository) : base(cardTypeRepository)
        {
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<CardTypeDTO>> GetCardTypes(bool onlyActive)
        {
            var cardTypes = await _repository.FilterAsync(filter: c => !onlyActive || c.Status == Status.Active, includeProperties: "Currency");
            return cardTypes.ProjectedAsCollection<CardTypeDTO>();
        }

        public override async Task<CardTypeDTO> ModifyAsync(CardTypeDTO item)
        {
            item.Status = Status.Active;
            _repository.Modify(item.ProjectedAs<CardType>());
            await _repository.UnitOfWork.CommitAsync();
            return item;
        }

        public async Task Delete(int id)
        {
            var cardType = await _repository.GetAsync(id);
            cardType.ArchiveAt = DateTime.Now;
            cardType.Status = Status.Inactive;
            _repository.Modify(cardType);
            await _repository.UnitOfWork.CommitAsync();
        }
        #endregion
    }
}
