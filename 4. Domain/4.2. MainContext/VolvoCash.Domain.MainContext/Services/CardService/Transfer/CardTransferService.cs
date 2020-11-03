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
                var displayNameFrom = messages.GetStringResource(LocalizationKeys.Domain.messages_TransferFromMessageDisplayName);
                displayNameFrom = string.Format(displayNameFrom, destinyCard.Contact.Phone);
                var descriptionFrom = messages.GetStringResource(LocalizationKeys.Domain.messages_TransferFromMessageDescription);
                descriptionFrom = string.Format(descriptionFrom, destinyCard.Contact.Phone);
                var movementBatchs = originCard.WithdrawMoney(
                    MovementType.STR,
                    transfer.Amount,
                    displayNameFrom,
                    descriptionFrom,
                    transfer
                );
                var displayNameTo = messages.GetStringResource(LocalizationKeys.Domain.messages_TransferToMessageDescription);
                displayNameTo = string.Format(displayNameTo, originCard.Contact.Phone);
                var descriptionTo = messages.GetStringResource(LocalizationKeys.Domain.messages_TransferToMessageDisplayName);
                descriptionTo = string.Format(descriptionTo, originCard.Contact.Phone);
                destinyCard.DepositMoneyFromTransfer(
                    transfer.Amount,
                    movementBatchs,
                    displayNameTo,
                    descriptionTo,
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
