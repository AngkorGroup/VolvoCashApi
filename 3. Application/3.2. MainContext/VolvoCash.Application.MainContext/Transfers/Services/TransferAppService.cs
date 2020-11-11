using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Transfers;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Utils;
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
        private readonly IAmazonBucketService _amazonService;
        private readonly IUrlManager _urlManager;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public TransferAppService(IContactRepository contactRepository,
                                  ICardRepository cardRepository,
                                  ITransferRepository transferRepository,
                                  ICardTransferService transferService,
                                  IAmazonBucketService amazonService,
                                  IUrlManager urlManager)
        {
            _contactRepository = contactRepository;
            _cardRepository = cardRepository;
            _transferRepository = transferRepository;
            _transferService = transferService;
            _amazonService = amazonService;
            _urlManager = urlManager;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Private Methods
        private async Task GenerateTransferImageUrl(Transfer transfer)
        {
            var url = _urlManager.GetTransferVoucherImageUrl(transfer.Id);
            transfer.ImageUrl = await _amazonService.UploadImageUrlToS3(url, ".png", "transfers");
            await _transferRepository.UnitOfWork.CommitAsync();
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
            var originCard = await _cardRepository.GetCardByIdWithBatchesAsync(transferDTO.OriginCardId,phone);     
            
            if (originCard == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_CardNotFound));
            }

            var destinyContact = await _contactRepository.GetContactById(transferDTO.DestinyCard.ContactId);    

            var amountToTransfer = new Money(transferDTO.Amount.Currency, transferDTO.Amount.Value);

            var transfer = await _transferService.PerformTransfer(originCard, destinyContact, amountToTransfer);

            _transferRepository.Add(transfer);

            await _transferRepository.UnitOfWork.CommitAsync();

            await GenerateTransferImageUrl(transfer);

            return transfer.ProjectedAs<TransferListDTO>();
        }

        public async Task<TransferDTO> GetTransferById(int id)
        {
            var transfer = await _transferRepository.GetTransferById(id);
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
