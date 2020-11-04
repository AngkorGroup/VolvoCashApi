using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Application.Seedwork;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Application.MainContext.Dealers.Services
{
    public class DealerAppService : Service<Dealer, DealerDTO>, IDealerAppService
    {
        #region Members
        private readonly IChargeRepository _chargeRepository;
        private readonly ICashierRepository _cashierRepository;
        #endregion

        #region Constructor
        public DealerAppService(IDealerRepository dealerRepository,
                                ICashierRepository cashierRepository,
                                IChargeRepository chargeRepository) : base(dealerRepository)
        {
            _chargeRepository = chargeRepository;
            _cashierRepository = cashierRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<CashierDTO>> GetDealerCashiers(int id)
        {
            var cashiers = await _cashierRepository.FilterAsync(filter: c => c.DealerId == id);
            return cashiers.ProjectedAsCollection<CashierDTO>();
        }

        public async Task<List<ChargeListDTO>> GetDealerCharges(int id, DateTime? beginDate, DateTime? endDate, int? cashierId, List<int> cardTypes)
        {
            var charges = await _chargeRepository.FilterAsync(filter: c => c.Cashier.DealerId == id
                    && (beginDate == null || c.CreatedAt >= beginDate)
                    && (endDate == null || c.CreatedAt <= endDate)
                    && (cashierId == null || c.Cashier.Id == cashierId)
                    && (cardTypes == null || cardTypes.Count == 0 || cardTypes.Contains(c.Card.CardTypeId))
                    , includeProperties: "Charge.Cashier.Dealer"
                    , orderBy: qc => qc.OrderByDescending(c => c.CreatedAt));
            return charges.ProjectedAsCollection<ChargeListDTO>();
        }

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
