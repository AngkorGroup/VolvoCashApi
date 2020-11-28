using Microsoft.Extensions.Logging;
using VolvoCash.Data.Seedwork;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;

namespace VolvoCash.Data.MainContext.Repositories
{
    public class DocumentTypeRepository : Repository<DocumentType, MainDbContext>, IDocumentTypeRepository
    {
        #region Constructor
        public DocumentTypeRepository(MainDbContext dbContext, ILogger<Repository<DocumentType, MainDbContext>> logger) : base(dbContext, logger)
        {
        }
        #endregion
    }
}
