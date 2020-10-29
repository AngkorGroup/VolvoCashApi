using System;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Domain.MainContext.Services.CardService
{
    public class CardTransferService : ICardTransferService
    {
        public void PerformTransfer(Transfer transfer)
        {
            var messages = LocalizationFactory.CreateLocalResources();
            var originCard = transfer.OriginCard;
            var destinyCard = transfer.DestinyCard;

            if (originCard is null)
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_PerformTransferOriginCardIsNull));

            if (destinyCard is null)
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_PerformTransferDestinyCardIsNull));

            if (originCard.Id == destinyCard.Id)
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_InvalidTransferToSameCard));

            if (originCard.Contact.ClientId != destinyCard.Contact.ClientId)
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_InvalidTransferDifferentContactClient));

            if (originCard.CanWithdraw(transfer.Amount))
            {
                var movementBatchs = originCard.WithdrawMoney(
                    transfer.Amount,
                    MovementType.STR,
                    messages.GetStringResource(LocalizationKeys.Domain.messages_TransferFromMessageDescription),
                    messages.GetStringResource(LocalizationKeys.Domain.messages_TransferFromMessageDisplayName),
                    movement : null,
                    transfer
                );
                destinyCard.DepositMoneyFromTransfer(
                    transfer.Amount,
                    movementBatchs,
                    messages.GetStringResource(LocalizationKeys.Domain.messages_TransferToMessageDescription),
                    messages.GetStringResource(LocalizationKeys.Domain.messages_TransferToMessageDisplayName),
                    transfer
                );
            }
            else
            {
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_NoEnoughMoneyToTransfer));
            }
            transfer.ImageUrl =  GenerateTransferUrl();
        }

        private string GenerateTransferUrl()
        {
            return "https://s3-us-east-2.amazonaws.com/volvocashbucket/charges/f7356fff-effb-4080-a95c-fb4f1b7300c7.jpg";
        }
    }
}
