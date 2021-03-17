using System;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public class CardRechargeService : ICardRechargeService
    {
        public void PerformRecharge (Card card , Batch batch)
        {
            var messages = LocalizationFactory.CreateLocalResources();
            if ( card != null )
            {
                var description = messages.GetStringResource(LocalizationKeys.Domain.messages_RechargeMessageDescription);
                var displayName = messages.GetStringResource(LocalizationKeys.Domain.messages_RechargeMessageDisplayName);
                description = string.Format(description, batch.BusinessArea?.Name, batch.TPChasis);
                displayName = string.Format(displayName, batch.BusinessArea?.Name, batch.TPChasis);
                card.RechargeMoney(
                    batch,
                    description,
                    displayName
                );
            }
            else
            {
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_PerformRechargeCardIsNull));
            }
        }
    }
}
