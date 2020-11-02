using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Application.MainContext.DTO.Movements;
using VolvoCash.Application.MainContext.Movements.Services;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Application.MainContext.Cards.Services
{
    public class MovementAppService : IMovementAppService
    {

        #region Members
        private readonly IMovementRepository _movementRepository;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public MovementAppService(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<MovementDTO>> GetMovementsByCardId(int cardId)
        {
            var movements = (await _movementRepository.FilterAsync(filter: m => m.CardId == cardId));

            if (movements != null && movements.Any())
            {
                return movements.ProjectedAsCollection<MovementDTO>();
            }
            return new List<MovementDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _movementRepository.Dispose();
        }
        #endregion
    }
}
