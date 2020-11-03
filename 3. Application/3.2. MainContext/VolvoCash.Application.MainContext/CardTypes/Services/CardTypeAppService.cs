using System;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

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
        public async Task Delete(int id)
        {
            var cardType = await _repository.GetAsync(id);
            cardType.ArchiveAt = DateTime.Now;
            cardType.Status = Domain.MainContext.Enums.Status.Inactive;
            _repository.Modify(cardType);
            await _repository.UnitOfWork.CommitAsync();
        }
        #endregion
    }
}
