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
                card.RechargeMoney(
                    batch,
                    messages.GetStringResource(LocalizationKeys.Domain.messages_RechargeMessageDescription),
                    messages.GetStringResource(LocalizationKeys.Domain.messages_RechargeMessageDisplayName)
                );
            }
            else
            {
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_PerformRechargeCardIsNull));
            }
        }
    }
}
