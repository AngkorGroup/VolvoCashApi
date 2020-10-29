using System;
using System.Linq;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public class CardChargeService : ICardChargeService
    {
        public void PerformCharge(Charge charge)
        {
            var card = charge.Card;
            var messages = LocalizationFactory.CreateLocalResources();
            if (card != null)
            {
                if (charge.Status != ChargeStatus.Pending)
                {
                    throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_InvalidStateForCharge));
                }
                if (card.CanWithdraw(charge.Amount))
                {
                    card.WithdrawMoney(charge.Amount,
                        MovementType.CON,
                        messages.GetStringResource(LocalizationKeys.Domain.messages_ChargeMessageDescription),
                        messages.GetStringResource(LocalizationKeys.Domain.messages_ChargeMessageDisplayName),
                        charge.Movements.FirstOrDefault()
                    );
                    charge.Status = ChargeStatus.Accepted;
                    charge.GenerateOperationCode();
                    charge.ImageUrl = GenerateChargeUrl();
                }
                else
                {
                    charge.Status = ChargeStatus.Canceled;
                }
            }
            else
            {
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_PerformChargeCardIsNull));
            }
        }
        private string GenerateChargeUrl()
        {
            return "https://s3-us-east-2.amazonaws.com/volvocashbucket/charges/f7356fff-effb-4080-a95c-fb4f1b7300c7.jpg";
        }
    }
}
