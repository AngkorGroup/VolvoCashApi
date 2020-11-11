using System.Collections.Generic;
using System.Threading.Tasks;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<Card> GetCardByIdWithBatchesAsync(int id);
        Task<Card> GetCardByIdWithBatchesAsync(int id,string phone);
        Task<IEnumerable<Card>> GetCardsByContactId(int id);
    }
}
