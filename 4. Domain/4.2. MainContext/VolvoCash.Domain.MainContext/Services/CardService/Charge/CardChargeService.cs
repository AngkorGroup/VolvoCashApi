using System;
using System.Linq;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.EnumAgg;

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
                if (charge.Status.Weight != 1)
                {
                    throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_InvalidStatusForCharge));
                }
                if (card.CanWithdraw(charge.Amount))
                {
                    card.WithdrawMoney(charge.Movements.FirstOrDefault(), charge.Amount);
                    charge.Status =new ChargeStatus("Accepted","###",2);
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
        private string GenerateChargeUrl()
        {
            return "https://s3-us-east-2.amazonaws.com/volvocashbucket/charges/f7356fff-effb-4080-a95c-fb4f1b7300c7.jpg";
        }
    }
}
