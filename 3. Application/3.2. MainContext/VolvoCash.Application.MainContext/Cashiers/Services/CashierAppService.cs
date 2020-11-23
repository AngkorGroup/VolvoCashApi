using System;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.Seedwork;
using VolvoCash.Application.Seedwork.Common;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Utils;
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
        private readonly IEmailManager _emailManager;
        #endregion

        #region Constructor
        public CashierAppService(ICashierRepository cashierRepository,
                                 IDealerRepository dealerRepository,
                                 IEmailManager emailManager) : base(cashierRepository)
        {
            _cashierRepository = cashierRepository;
            _dealerRepository = dealerRepository;
            _emailManager = emailManager;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Private Methods
        private void SendEmailToCashier(CashierDTO cashierDTO)
        {
            var subject = _resources.GetStringResource(LocalizationKeys.Application.messages_NewCashierEmailSubject);
            var body = _resources.GetStringResource(LocalizationKeys.Application.messages_NewCashierEmailBody);
            body = string.Format(body, cashierDTO.Password);
            _emailManager.SendEmail(cashierDTO.Email, subject, body);
        }
        #endregion

        #region ApiPOS Public Methods
        public async Task<CashierDTO> GetByUserId(int userId)
        {
            var cashier = (await _repository.FilterAsync(filter: c=> c.UserId == userId)).FirstOrDefault();
            return cashier.ProjectedAs<CashierDTO>();
        }
        #endregion

        #region ApiWeb Public Methods
        public override async Task<CashierDTO> AddAsync(CashierDTO cashierDTO)
        {
            var dealer = await _dealerRepository.GetAsync(cashierDTO.DealerId);
            if (dealer == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidDealerForCashier));
            }
            var existingCashier =  _cashierRepository.Filter(c => (c.Email == cashierDTO.Email || c.Phone == cashierDTO.Phone) && c.ArchiveAt == null).FirstOrDefault();
            if (existingCashier != null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CashierAlreadyExists));
            }
            cashierDTO.Password = RandomGenerator.RandomDigits(6);
            var cashier = new Cashier(dealer, cashierDTO.FirstName, cashierDTO.LastName, cashierDTO.Password, cashierDTO.Phone, cashierDTO.TPCode, cashierDTO.Email, cashierDTO.Imei);
            _repository.Add(cashier);
            await _repository.UnitOfWork.CommitAsync();
            SendEmailToCashier(cashierDTO);
            return cashier.ProjectedAs<CashierDTO>();
        }

        public override async Task<CashierDTO> ModifyAsync(CashierDTO cashierDTO)
        {
            var cashier = await _repository.GetAsync(cashierDTO.Id);
            if (cashier == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CashierNotFound));
            }

            var existingCashier = _cashierRepository.Filter(c => (c.Email == cashierDTO.Email || c.Phone == cashierDTO.Phone) && c.ArchiveAt == null).FirstOrDefault();
            if (existingCashier.Id != cashierDTO.Id)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CashierAlreadyExists));
            }

            cashier.Email = cashierDTO.Email;
            cashier.FirstName = cashierDTO.FirstName;
            cashier.Imei = cashierDTO.Imei;
            cashier.LastName = cashierDTO.LastName;
            cashier.Phone = cashierDTO.Phone;
            cashier.TPCode = cashierDTO.TPCode;

            _repository.Modify(cashier);
            await _repository.UnitOfWork.CommitAsync();
            return cashier.ProjectedAs<CashierDTO>();
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
