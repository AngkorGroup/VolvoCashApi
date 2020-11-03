using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public interface IMovementRepository : IRepository<Movement>
    {
        #region Public Methods
        Task<IEnumerable<Movement>> GetMovementsByCard(int cardId);
        #endregion
    }
}
