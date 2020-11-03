using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Services.CardService;

namespace VolvoCash.Application.MainContext.Charges.Services
{
    public class ChargeAppService : IChargeAppService
    {

        #region Members
        private readonly IChargeRepository _chargeRepository;
        private readonly ICardChargeService _cardChargeService;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public ChargeAppService(IChargeRepository chargeRepository,
                                ICardChargeService cardChargeService)
        {
            _chargeRepository = chargeRepository;
            _cardChargeService = cardChargeService;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<List<ChargeDTO>> GetChargesByPhone(string phone)
        {
            var charges = await _chargeRepository.FilterAsync(c => c.Card.Contact.Phone == phone && c.Status == ChargeStatus.Pending);
            return charges.ProjectedAsCollection<ChargeDTO>();
        }

        public async Task<ChargeDTO> GetChargeByPhone(string phone, int id)
        {
            var charge = (await _chargeRepository.FilterAsync(c => c.Card.Contact.Phone == phone && c.Id == id)).FirstOrDefault();
            return charge.ProjectedAs<ChargeDTO>();
        }

        public async Task<ChargeDTO> PerformChargeByPhone(string phone, int chargeId, bool confirmed)
        {
            var charge = _chargeRepository.Filter(filter: c => c.Id == chargeId && c.Card.Contact.Phone == phone, includeProperties: "Card.Contact.Client,Card.CardBatches.Batch,Card.CardType").FirstOrDefault();
            if (charge == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_ChargeNotFound));
            }
            if (confirmed)
            {
                _cardChargeService.PerformCharge(charge);
            }
            else
            {
                charge.Status = ChargeStatus.Rejected;
            }
            _chargeRepository.Modify(charge);
            await _chargeRepository.UnitOfWork.CommitAsync();
            //TODO enviar push notification a el cajero indicando el estado de la transaccion
            return charge.ProjectedAs<ChargeDTO>();
        }
        #endregion

        #region ApiPOS Public Methods
        public async Task<List<ChargeDTO>> GetChargesByCashierId(int id, ChargeType chargeType, int pageIndex, int pageLength)
        {
            var charges = await _chargeRepository.GetFilteredAsync(
                c => c.Cashier.Id == id && c.ChargeType == chargeType, pageIndex, pageLength, c => c.CreatedAt, false);
            return charges.ProjectedAsCollection<ChargeDTO>();
        }

        public async Task<ChargeDTO> AddCharge(ChargeDTO chargeDTO)
        {
            var charge = new Charge(
                chargeDTO.CashierId,
                chargeDTO.CardId,
                chargeDTO.ChargeType,
                new Money(chargeDTO.Amount.Currency, chargeDTO.Amount.Value),
                _resources.GetStringResource(LocalizationKeys.Application.messages_CreateChargeDisplayName),
                chargeDTO.Description
            );
            _chargeRepository.Add(charge);
            await _chargeRepository.UnitOfWork.CommitAsync();
            //TODO enviar push notification a contacto  indicando que tiene un cobro pendiente
            return charge.ProjectedAs<ChargeDTO>();
        }
        #endregion

        #region Common Public Method
        public async Task<ChargeDTO> GetChargeById(int id)
        {
            var charge = (await _chargeRepository.FilterAsync(filter: c => c.Id == id, includeProperties: "Cashier")).FirstOrDefault();
            return charge.ProjectedAs<ChargeDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _chargeRepository.Dispose();
        }
        #endregion
    }
}
