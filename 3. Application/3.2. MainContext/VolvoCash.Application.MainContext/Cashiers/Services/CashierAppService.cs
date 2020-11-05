using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.Seedwork;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.Cashiers.Services
{
    public class CashierAppService : Service<Cashier, CashierDTO>, ICashierAppService
    {
        #region Members
        private readonly ICashierRepository _cashierRepository;
        private readonly IDealerRepository _dealerRepository;
        #endregion

        #region Constructor
        public CashierAppService(ICashierRepository cashierRepository,
                                 IDealerRepository dealerRepository) : base(cashierRepository)
        {
            _cashierRepository = cashierRepository;
            _dealerRepository = dealerRepository;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiWeb Public Methods
        public override async Task<CashierDTO> AddAsync(CashierDTO item)
        {
            var dealer = await _dealerRepository.GetAsync(item.DealerId);
            if (dealer == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidDealerForCashier));
            }
            var existingCashier =  _cashierRepository.Filter(c => (c.Email == item.Email || c.Phone == item.Phone) && c.ArchiveAt == null).FirstOrDefault();
            if (existingCashier != null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CashierAlreadyExistsEmail));
            }
            item.Password = RandomGenerator.RandomDigits(6);
            var cashier = new Cashier(dealer,item.FirstName,item.LastName,item.Password,item.Phone,item.TPCode,item.Email,item.Imei);
            _repository.Add(cashier);
            await _repository.UnitOfWork.CommitAsync();
            //TODO enviar correo con credenciales
            return cashier.ProjectedAs<CashierDTO>();
        }

        public override async Task<CashierDTO> ModifyAsync(CashierDTO item)
        {
            var cashier = await _repository.GetAsync(item.Id);
            if (cashier == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CashierNotFound));
            }

            var existingCashier = _cashierRepository.Filter(c => (c.Email == item.Email || c.Phone == item.Phone) && c.ArchiveAt == null).FirstOrDefault();
            if (existingCashier.Id != item.Id)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CashierAlreadyExistsEmail));
            }
            
            item.Status = Status.Active;
            item.PasswordHash = cashier.PasswordHash;
            item.DealerId = cashier.DealerId;
            _repository.Modify(item.ProjectedAs<Cashier>());
            await _repository.UnitOfWork.CommitAsync();
            return item;
        }

        public async Task Delete(int id)
        {
            var cashier = await _repository.GetAsync(id);
            cashier.ArchiveAt = DateTime.Now;
            cashier.Status = Status.Inactive;
            _repository.Modify(cashier);
            await _repository.UnitOfWork.CommitAsync();
        }
        #endregion
    }
}
