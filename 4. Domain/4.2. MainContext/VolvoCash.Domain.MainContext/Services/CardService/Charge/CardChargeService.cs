using System;
using System.Collections.Generic;
using System.Linq;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public class CardChargeService : ICardChargeService
    {
        private string GetBatchDetail(List<BatchMovement> batchMovements)
        {
            return string.Join(",", batchMovements.Select(bm => $"Chasis # {bm.Batch.TPChasis}: {bm.Amount.GetLabel()}"));
        }
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
                    var batchMovements = card.WithdrawMoney(charge.Movements.FirstOrDefault(), charge.Amount);
                    charge.BatchesDetail = GetBatchDetail(batchMovements);
                    charge.Status = ChargeStatus.Accepted;
                    charge.GenerateOperationCode();
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
