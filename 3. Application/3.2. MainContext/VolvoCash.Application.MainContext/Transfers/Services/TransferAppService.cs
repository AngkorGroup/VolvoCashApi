using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Transfers;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Services.CardService;

namespace VolvoCash.Application.MainContext.Transfers.Services
{
    public class TransferAppService : ITransferAppService
    {

        #region Members
        private readonly IContactRepository _contactRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly ICardTransferService _transferService;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public TransferAppService(IContactRepository contactRepository,
                                  ICardRepository cardRepository,
                                  ITransferRepository transferRepository,
                                  ICardTransferService transferService)
        {
            _contactRepository = contactRepository;
            _cardRepository = cardRepository;
            _transferRepository = transferRepository;
            _transferService = transferService;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region ApiClient Public Methods
        public async Task<List<TransferListDTO>> GetTransfersByPhone(string phone)
        {
            var transfers = await _transferRepository.FilterAsync(t => t.OriginCard.Contact.Phone == phone);
            return transfers.ProjectedAsCollection<TransferListDTO>();
        }

        public async Task<TransferListDTO> PerformTransfer(string phone, TransferDTO transferDTO)
        {
            var originCard = (await _cardRepository.FilterAsync(filter: c => c.Id == transferDTO.OriginCardId && c.Contact.Phone == phone,
                                    includeProperties: "Contact.Client,CardBatches.Batch,CardType")).FirstOrDefault();           
            if (originCard == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CardNotFound));
            }
            var destinyContact = (await _contactRepository.FilterAsync(filter: (c) => c.Id == transferDTO.DestinyCard.ContactId, includeProperties: "Cards.CardBatches.Batch")).FirstOrDefault();
            var destinyCard = destinyContact.Cards.FirstOrDefault((c) => c.CardTypeId == originCard.CardTypeId);            
            if (destinyCard == null)
            {
                destinyCard = new Card(destinyContact, originCard.CardType.Currency, originCard.CardTypeId);
            }
            var displayName = _resources.GetStringResource(LocalizationKeys.Application.messages_CreateTransferDisplayName);
            displayName = string.Format(displayName, originCard.Contact.Phone, destinyCard.Contact.Phone);
            var transfer = new Transfer(originCard, destinyCard, new Money(transferDTO.Amount.Currency, transferDTO.Amount.Value), displayName);
            destinyCard.DestinyTransfers.Add(transfer);
            _transferService.PerformTransfer(transfer);          
            if (destinyCard.Id == 0)
            {
                _cardRepository.Add(destinyCard);
            }
            else
            {
                _cardRepository.Modify(destinyCard);
            }
            await _cardRepository.UnitOfWork.CommitAsync();
            return transfer.ProjectedAs<TransferListDTO>();
        }

        public async Task<TransferDTO> GetTransferById(int id)
        {
            var transfer = (await _transferRepository.FilterAsync(
                filter: c => c.Id == id,
                includeProperties: "OriginCard.Contact,DestinyCard.Contact")
            ).FirstOrDefault();
            return transfer.ProjectedAs<TransferDTO>();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _cardRepository.Dispose();
            _contactRepository.Dispose();
            _cardRepository.Dispose();
        }
        #endregion
    }
}
