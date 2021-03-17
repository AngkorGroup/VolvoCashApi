using System.Threading.Tasks;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.Pushs;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public class  NotificationChargeService : INotificationChargeService
    {
        #region Members
        private readonly ISessionRepository _sessionRepository;
        private readonly IPushNotificationManager _pushNotificationManager;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public NotificationChargeService(ISessionRepository sessionRepository,
                                         IPushNotificationManager pushNotificationManager)
        {
            _sessionRepository = sessionRepository;
            _pushNotificationManager = pushNotificationManager; 
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Public Methods
        public async Task SendNotificationToCashier(Charge charge)
        {
            var deviceTokens = await _sessionRepository.GetActivePushDeviceTokensAsync(charge.Cashier.UserId);
            var heading = "";
            var content = "";
            switch (charge.Status)
            {
                case ChargeStatus.Accepted:
                    heading = _resources.GetStringResource(LocalizationKeys.Domain.messages_AcceptedChargeHeadingToCashier);
                    content = _resources.GetStringResource(LocalizationKeys.Domain.messages_AcceptedChargeContentToCashier);
                    break;
                case ChargeStatus.Rejected:
                    heading = _resources.GetStringResource(LocalizationKeys.Domain.messages_RejectedChargeHeadingToCashier);
                    content = _resources.GetStringResource(LocalizationKeys.Domain.messages_RejectedChargeContentToCashier);
                    break;
                case ChargeStatus.Canceled:
                    heading = _resources.GetStringResource(LocalizationKeys.Domain.messages_CanceledChargeHeadingToCashier);
                    content = _resources.GetStringResource(LocalizationKeys.Domain.messages_CanceledChargeContentToCashier);
                    break;
                default:
                    break;
            }
            content = string.Format(content, charge.Amount.GetLabel(), charge.DisplayName.Replace("Cobro", ""));
            _pushNotificationManager.SendNotifications(AppType.Cashier,deviceTokens, heading, content, new { chargeId = charge.Id });
        }

        public async Task SendNotificationToContact(Charge charge)
        {
            var deviceTokens = await _sessionRepository.GetActivePushDeviceTokensByPhoneAsync(charge.Card.Contact.Phone);
            var heading = _resources.GetStringResource(LocalizationKeys.Domain.messages_OnCreatingChargeHeadingToContact);
            var content = _resources.GetStringResource(LocalizationKeys.Domain.messages_OnCreatingChargeContentToContact);
            content = string.Format(content, charge.Amount.GetLabel());
            _pushNotificationManager.SendNotifications(AppType.Client, deviceTokens, heading, content, new { chargeId = charge.Id });
        }
        #endregion
    }
}
