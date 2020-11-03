using System;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;

namespace VolvoCash.Application.MainContext.Dealers.Services
{
    public class DealerAppService : Service<Dealer, DealerDTO>, IDealerAppService
    {
        #region Members
        #endregion

        #region Constructor
        public DealerAppService(IDealerRepository dealerRepository) : base(dealerRepository)
        {
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task Delete(int id)
        {
            var dealer = await _repository.GetAsync(id);
            dealer.ArchiveAt = DateTime.Now;
            dealer.Status = Domain.MainContext.Enums.Status.Inactive;
            _repository.Modify(dealer);
            await _repository.UnitOfWork.CommitAsync();
        }
        #endregion

    }
}
