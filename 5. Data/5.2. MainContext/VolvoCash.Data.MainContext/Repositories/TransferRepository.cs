using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class TransferRepository : Repository<Transfer, MainDbContext>, ITransferRepository
    {
        #region Constructor
        public TransferRepository(MainDbContext dbContext,
                                  ILogger<Repository<Transfer, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion

        #region Public Methods
        public async Task<Transfer> GetTransferById(int id)
        {
            return (await FilterAsync( filter: c => c.Id == id, includeProperties: "OriginCard.CardType,OriginCard.Contact,DestinyCard.Contact")).FirstOrDefault();
        }
        #endregion
    }
}
