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
                    throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_InvalidStatusForCharge));
                }
                if (card.CanWithdraw(charge.Amount))
                {
                    card.WithdrawMoney(charge.Movements.FirstOrDefault(), charge.Amount);
                    charge.Status = ChargeStatus.Accepted;
                    charge.GenerateOperationCode();
                    //charge.ImageUrl = GenerateChargeUrl();
                }
                else
                {
                    if (charge.Id == 0)
                    {
                        throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_NoEnoughMoneyToWithdraw));
                    }
                    else
                    {
                        throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_NoEnoughMoneyToWithdraw));
                        //if the charge already exists only cancel it
                        //charge.Status = ChargeStatus.Canceled;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_PerformChargeCardIsNull));
            }
        }
    }
}
