using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.BankAccounts;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Application.Seedwork;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Dealers.Services
{
    public class DealerAppService : Service<Dealer, DealerDTO>, IDealerAppService
    {
        #region Members
        private readonly IChargeRepository _chargeRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        #endregion

        #region Constructor
        public DealerAppService(IDealerRepository dealerRepository,
                                ICashierRepository cashierRepository,
                                IChargeRepository chargeRepository,
                                IBankAccountRepository bankAccountRepository) : base(dealerRepository)
        {
            _chargeRepository = chargeRepository;
            _cashierRepository = cashierRepository;
            _bankAccountRepository = bankAccountRepository;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<DealerDTO>> GetDealers(string query, int maxRecords)
        {
            query = query?.Trim().ToUpper();
            var dealers = await _repository.FilterAsync(filter: d => d.Name.Trim().ToUpper().Contains(query)
                                                                  || d.Ruc.Trim().ToUpper().Contains(query)
                                                                  || string.IsNullOrEmpty(query));
            dealers = dealers.Take(Math.Min(dealers.Count(), maxRecords));
            return dealers.ProjectedAsCollection<DealerDTO>();
        }

        public async Task<List<CashierDTO>> GetDealerCashiers(int id, bool onlyActive)
        {
            var cashiers = await _cashierRepository.FilterAsync(filter: c => c.DealerId == id && (!onlyActive || c.Status == Status.Active));
            return cashiers.ProjectedAsCollection<CashierDTO>();
        }

        public async Task<List<ChargeListDTO>> GetDealerCharges(int id, DateTime? beginDate, DateTime? endDate, int? cashierId, List<int> cardTypes)
        {
            var charges = await _chargeRepository.FilterAsync(filter: c => c.Cashier.DealerId == id
                    && (beginDate == null || c.CreatedAt >= beginDate)
                    && (endDate == null || c.CreatedAt <= endDate)
                    && (cashierId == null || c.Cashier.Id == cashierId)
                    && (cardTypes == null || cardTypes.Count == 0 || cardTypes.Contains(c.Card.CardTypeId))
                    , includeProperties: "Cashier.Dealer,Card.CardType,Card.Contact.Client"
                    , orderBy: qc => qc.OrderByDescending(c => c.CreatedAt));
            return charges.ProjectedAsCollection<ChargeListDTO>();
        }

        public async Task Delete(int id)
        {
            var dealer = await _repository.GetAsync(id);
            dealer.ArchiveAt = DateTime.Now;
            dealer.Status = Status.Inactive;
            _repository.Modify(dealer);
            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task<List<BankAccountDTO>> GetBankAccounts(int id, bool onlyActive)
        {
            var bankAccounts = await _bankAccountRepository.FilterAsync(
                filter: ba => (!onlyActive || ba.Status == Status.Active) && ba.DealerId == id,
                includeProperties: "Bank,BankAccountType,Currency",
                orderBy: ba => ba.OrderByDescending(x => x.CreatedAt)
            );
            return bankAccounts.ProjectedAsCollection<BankAccountDTO>();
        }
        #endregion
    }
}
