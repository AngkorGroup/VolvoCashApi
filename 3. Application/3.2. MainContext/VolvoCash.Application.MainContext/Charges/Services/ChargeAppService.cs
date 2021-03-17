using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Services.CardService;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using System.Runtime.Serialization;

namespace VolvoCash.Application.MainContext.Charges.Services
{
    public class ChargeAppService : IChargeAppService
    {

        #region Members
        private readonly IChargeRepository _chargeRepository;
        private readonly ICardBatchRepository _cardBatchRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ICardChargeService _cardChargeService;
        private readonly INotificationChargeService _notificationChargeService;
        private readonly IAmazonBucketService _amazonService;
        private readonly IUrlManager _urlManager;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public ChargeAppService(IChargeRepository chargeRepository,
                                ICardBatchRepository cardBatchRepository,
                                ICurrencyRepository currencyRepository,
                                ICardRepository cardRepository,
                                ICardChargeService cardChargeService,
                                INotificationChargeService notificationChargeService,
                                IAmazonBucketService amazonService,
                                IUrlManager urlManager)
        {
            _chargeRepository = chargeRepository;
            _cardBatchRepository = cardBatchRepository;
            _currencyRepository = currencyRepository;
            _cardRepository = cardRepository;
            _cardChargeService = cardChargeService;
            _notificationChargeService = notificationChargeService;
            _amazonService = amazonService;
            _urlManager = urlManager;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<List<ChargeDTO>> GetChargesByPhone(string phone)
        {
            var charges = await _chargeRepository.FilterAsync(c => c.Card.Contact.Phone == phone && c.Status == ChargeStatus.Pending, includeProperties: "Cashier");
            return charges.ProjectedAsCollection<ChargeDTO>();
        }

        public async Task<ChargeDTO> GetChargeByPhone(string phone, int id)
        {
            var charge = (await _chargeRepository.FilterAsync(c => c.Card.Contact.Phone == phone && c.Id == id, includeProperties: "Cashier")).FirstOrDefault();
            return charge.ProjectedAs<ChargeDTO>();
        }

        public async Task<ChargeDTO> PerformChargeByPhone(string phone, int chargeId, bool confirmed)
        {
            var charge = _chargeRepository.Filter(
                filter: c => c.Id == chargeId && c.Card.Contact.Phone == phone,
                includeProperties: "Card.Contact.Client,Card.CardType,Movements,Cashier,Amount.Currency").FirstOrDefault();
            charge.Card.CardBatches = _cardBatchRepository.Filter(filter: c => c.CardId == charge.CardId, includeProperties: "Batch.Balance.Currency,Balance.Currency").ToList();
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
            await GenerateChargeImageUrl(charge);
            await _notificationChargeService.SendNotificationToCashier(charge);
            return charge.ProjectedAs<ChargeDTO>();
        }

        private async Task GenerateChargeImageUrl(Charge charge)
        {
            if (charge.Status == ChargeStatus.Accepted)
            {
                var url = _urlManager.GetChargeVoucherImageUrl(charge.Id);
                charge.ImageUrl = await _amazonService.UploadImageUrlToS3(url, ".png", "charges");
                await _chargeRepository.UnitOfWork.CommitAsync();
            }
        }
        #endregion

        #region ApiPOS Public Methods
        public async Task<List<ChargeListDTO>> GetChargesByCashierId(int id, ChargeType chargeType, int pageIndex, int pageLength)
        {
            var charges = await _chargeRepository.GetFilteredAsync(c => c.Cashier.Id == id && c.ChargeType == chargeType,
                                                                        pageIndex, pageLength, c => c.CreatedAt, false);
            return charges.ProjectedAsCollection<ChargeListDTO>();
        }

        public string GetEnumMemberAttrValue<T>(T enumVal)
        {
            var enumType = typeof(T);
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attr != null)
            {
                return attr.Value;
            }

            return null;
        }

        public async Task<ChargeDTO> AddCharge(ChargeDTO chargeDTO)
        {
            var card = _cardRepository.Filter(filter: c => c.Id == chargeDTO.CardId, includeProperties: "Contact.Client,CardBatches.Batch,CardType").FirstOrDefault();

            var displayName = _resources.GetStringResource(LocalizationKeys.Application.messages_CreateChargeDisplayName);
            var clientName = card.Contact.Client.Name;
            var contactName = card.Contact.FullName;
            clientName = clientName.Substring(0, Math.Min(20, clientName.Length));
            contactName = contactName.Substring(0, Math.Min(20, contactName.Length));
            displayName = string.Format(displayName, GetEnumMemberAttrValue(chargeDTO.ChargeType), $"{card.Contact.Client.Ruc} / {clientName}" ,contactName );

            var chargeCurrency = _currencyRepository.Filter(c => c.Id == chargeDTO.Amount.CurrencyId).FirstOrDefault();

            var charge = new Charge(
                chargeDTO.CashierId,
                card,
                chargeDTO.ChargeType,
                new Money(chargeCurrency, chargeDTO.Amount.Value),
                displayName,
                chargeDTO.Description
            );

            _chargeRepository.Add(charge);
            await _chargeRepository.UnitOfWork.CommitAsync();

            await _notificationChargeService.SendNotificationToContact(charge);

            return charge.ProjectedAs<ChargeDTO>();
        }
        #endregion

        #region Common Public Method
        public async Task<ChargeDTO> GetChargeById(int id)
        {
            var charge = (await _chargeRepository.FilterAsync(filter: c => c.Id == id, includeProperties: "Cashier.Dealer,Card.Contact.DocumentType")).FirstOrDefault();
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
