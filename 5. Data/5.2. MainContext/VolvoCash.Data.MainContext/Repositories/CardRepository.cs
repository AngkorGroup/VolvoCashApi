using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class CardRepository : Repository<Card, MainDbContext>, ICardRepository
    {
        #region Constructor
        public CardRepository(MainDbContext dbContext,
                              ILogger<Repository<Card, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion

        #region Public Methods
        public async Task<Card> GetCardByIdWithBatchesAsync(int id)
        {
            return (await FilterAsync(filter: c => c.Id == id, includeProperties: "Contact.Client,CardBatches.Batch,CardType")).FirstOrDefault();
        }

        public async Task<Card> GetCardByIdWithBatchesAsync(int id, string phone)
        {
            return (await FilterAsync(filter: c => c.Id == id && c.Contact.Phone == phone && c.Contact.Status==Status.Active , includeProperties: "Contact.Client,CardBatches.Batch,CardType")).FirstOrDefault(); 
        }

        public async Task<IEnumerable<Card>> GetCardsByContactId(int contactId)
        {
            return await FilterAsync(filter: c => c.ContactId == contactId, includeProperties: "CardType");
        }
        #endregion
    }
}
