using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class ClientRepository : Repository<Client, MainDbContext>, IClientRepository
    {
        #region Constructor
        public ClientRepository(MainDbContext dbContext,
                                ILogger<Repository<Client, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
