using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class ResetPasswordTokenRepository : Repository<ResetPasswordToken, MainDbContext>, IResetPasswordTokenRepository
    {
        #region Members
        #endregion

        #region Constructor
        public ResetPasswordTokenRepository(MainDbContext dbContext, 
                                            ILogger<Repository<ResetPasswordToken, MainDbContext>> logger) 
            : base(dbContext, logger)
        {
        }
        #endregion
    }
}
