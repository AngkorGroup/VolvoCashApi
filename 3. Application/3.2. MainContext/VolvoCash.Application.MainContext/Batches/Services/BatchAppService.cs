using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Batches.Services;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.CardBatches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.MainContext.DTO.POJOS;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.CrossCutting.Utils.Constants;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Services.CardService;

namespace VolvoCash.Application.MainContext.Cards.Services
{
    public class BatchAppService : IBatchAppService
    {

        #region Members
        private readonly IClientRepository _clientRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ICardTypeRepository _cardTypeRepository;
        private readonly IRechargeTypeRepository _rechargeTypeRepository;
        private readonly IBusinessAreaRepository _businessAreaRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBatchErrorRepository _batchErrorRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly ICardRechargeService _rechargeService;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public BatchAppService(IClientRepository clientRepository,
                               IContactRepository contactRepository,
                               ICardRepository cardRepository,
                               ICardTypeRepository cardTypeRepository,
                               IRechargeTypeRepository rechargeTypeRepository,
                               IBusinessAreaRepository businessAreaRepository,
                               IBatchRepository batchRepository,
                               ICurrencyRepository currencyRepository,
                               IBatchErrorRepository batchErrorRepository,
                               IDocumentTypeRepository documentTypeRepository,
                               ICardRechargeService rechargeService)
        {
            _clientRepository = clientRepository;
            _contactRepository = contactRepository;
            _cardTypeRepository = cardTypeRepository;
            _rechargeTypeRepository = rechargeTypeRepository;
            _businessAreaRepository = businessAreaRepository;
            _cardRepository = cardRepository;
            _batchRepository = batchRepository;
            _currencyRepository = currencyRepository;
            _batchErrorRepository = batchErrorRepository;
            _documentTypeRepository = documentTypeRepository;
            _rechargeService = rechargeService;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Private Methods
        public IEnumerable<string> ReadLines(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
        private string GetLineSegment(string[] lineSegments, int columnIndex)
        {
            return lineSegments[columnIndex]?.Trim();
        }
        private Load GetLoadFromLine(string line, int rowIndex)
        {
            try
            {

                var lineSegments = line.Split("|");
                var batchTPCode = GetLineSegment(lineSegments, 0);
                var clientTPCode = GetLineSegment(lineSegments, 1);
                var clientName = GetLineSegment(lineSegments, 2);
                var clientEmail = GetLineSegment(lineSegments, 3);
                var contractDate = DateTimeParser.TryParseString(GetLineSegment(lineSegments, 4), DateTimeFormats.DateFormat);
                var chasisNumber = GetLineSegment(lineSegments, 5);
                var invoiceDocumentNumber = GetLineSegment(lineSegments, 6);
                var invoiceDate = DateTimeParser.TryParseString(GetLineSegment(lineSegments, 7), DateTimeFormats.DateFormat);
                var batchAmount = double.Parse(GetLineSegment(lineSegments, 8));
                //var batchCurrency = GetLineSegment(lineSegments, 9);
                var contactName = GetLineSegment(lineSegments, 10);
                var rechargeTypeCode = GetLineSegment(lineSegments, 11);
                var contractNumber = GetLineSegment(lineSegments, 12);
                var clientRuc = GetLineSegment(lineSegments, 13);
                var contactPhone = GetLineSegment(lineSegments, 14);
                var contactEmail = GetLineSegment(lineSegments, 15);
                var contactDocumentNumber = GetLineSegment(lineSegments, 16);
                var dealerCode = GetLineSegment(lineSegments, 17);
                var dealerName = GetLineSegment(lineSegments, 18);
                var businessCode = GetLineSegment(lineSegments, 19);
                var businessDescription = GetLineSegment(lineSegments, 20);
                var cardTypeCode = GetLineSegment(lineSegments, 21);
                var cardTypeDescription = GetLineSegment(lineSegments, 22);
                var documentTypeId = GetLineSegment(lineSegments, 23);
                var contactLastName = GetLineSegment(lineSegments, 24);
                var clientAddress = "-";
                var clientPhone = "-";
                var contactDocumentTypeId = (_documentTypeRepository.Filter(filter: dt => dt.TPCode == documentTypeId)).FirstOrDefault().Id;

                try
                {
                    var client = new ClientListDTO()
                    {
                        Ruc = clientRuc,
                        Address = clientAddress,
                        Email = clientEmail,
                        Name = clientName,
                        Phone = clientPhone,
                        TPCode = clientTPCode
                    };

                    var contact = new ContactDTO()
                    {
                        FirstName = contactName,
                        LastName = contactLastName,
                        Phone = contactPhone,
                        Email = contactEmail,
                        DocumentTypeId = contactDocumentTypeId,
                        DocumentNumber = contactDocumentNumber,
                    };

                    var cardType = _cardTypeRepository.Filter(ct => ct.TPCode == cardTypeCode).FirstOrDefault();

                    if (cardType == null)
                        throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidCardTypeCode));

                    var rechargeType = _rechargeTypeRepository.Filter(rt => rt.TPCode == rechargeTypeCode).FirstOrDefault();

                    if (rechargeType == null)
                        throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidRechargeTypeCode));

                    var businessArea = _businessAreaRepository.Filter(ba => ba.TPCode == businessCode).FirstOrDefault();

                    if (businessArea == null)
                        throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidBusinessAreaCode));

                    var currency = _currencyRepository.Filter(c => c.Id == cardType.CurrencyId).FirstOrDefault();

                    var existingBatch = _batchRepository.Filter(b => b.TPContractBatchNumber == batchTPCode && b.TPChasis == chasisNumber).FirstOrDefault();

                    if (existingBatch != null)
                    {
                        var existingBatchMessage = _resources.GetStringResource(LocalizationKeys.Application.exception_BatchAlreadyExists);
                        existingBatchMessage = string.Format(existingBatchMessage, batchTPCode, chasisNumber);
                        throw new InvalidOperationException(existingBatchMessage);
                    }

                    var cardTypeDTO = new CardTypeDTO()
                    {
                        DisplayName = cardType.DisplayName,
                        Name = cardType.Name
                    };

                    var card = new CardDTO()
                    {
                        CardTypeId = cardType.Id,
                        TPCode = batchTPCode,
                        CardType = cardTypeDTO
                    };

                    var currencyDTO = currency.ProjectedAs<CurrencyDTO>();

                    var amount = new MoneyDTO() { Currency = currencyDTO, CurrencyId = currencyDTO.Id, Value = batchAmount };

                    var batch = new BatchDTO()
                    {
                        Amount = amount,
                        TPContractDate = contractDate,
                        TPChasis = chasisNumber,
                        TPInvoiceDate = invoiceDate,
                        TPInvoiceCode = invoiceDocumentNumber,
                        RechargeTypeId = rechargeType.Id,
                        TPContractNumber = contractNumber,
                        TPContractBatchNumber = batchTPCode,
                        DealerCode = dealerCode,
                        DealerName = dealerName,
                        BusinessAreaId = businessArea.Id,
                        CardTypeId = cardType.Id,
                        LineContent = line,
                        Client = client,
                        Balance = amount,
                        CardType = cardTypeDTO,
                        RechargeType = new DTO.RechargeTypes.RechargeTypeDTO()
                        {
                            Name = rechargeType.Name,
                            TPCode = rechargeType.TPCode
                        },
                        BusinessArea = new DTO.BusinessAreas.BusinessAreaDTO()
                        {
                            Name = businessArea.Name
                        }
                    };

                    var load = new Load()
                    {
                        Batch = batch,
                        Card = card,
                        Client = client,
                        Contact = contact,
                        RowIndex = rowIndex
                    };

                    return load;
                }
                catch (Exception e)
                {
                    return new Load()
                    {
                        ErrorMessage = e.Message,
                        RowIndex = rowIndex,
                        LineContent = line
                    };
                }
            }
            catch (Exception e)
            {
                return new Load()
                {
                    ErrorMessage = "La estructura de la fila no sigue el formato determinado revisar su contenido.",
                    RowIndex = rowIndex,
                    LineContent = line
                };
            }
        }
        private List<Load> GetLoadsFromStream(Stream stream)
        {
            var loads = new List<Load>();
            var rowIndex = 1;
            foreach (var line in ReadLines(stream))
            {
                if (!string.IsNullOrEmpty(line?.Trim()))
                {
                    loads.Add(GetLoadFromLine(line, rowIndex));
                }
                rowIndex++;
            }
            return loads;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<BatchDTO>> GetBatches(DateTime? beginDate, DateTime? endDate)
        {
            var batches = await _batchRepository.FilterAsync(
                filter: b => (beginDate == null || b.CreatedAt.Date >= beginDate) &&
                             (endDate == null || b.CreatedAt.Date <= endDate),
                includeProperties: "Client.Contacts,CardType,RechargeType,BusinessArea",
                orderBy: bq => bq.OrderByDescending(b => b.CreatedAt));
            return batches.ProjectedAsCollection<BatchDTO>();
        }

        public async Task<List<BatchDTO>> GetBatchesByExpiresAtExtent(string clientId, DateTime? beginDate, DateTime? endDate)
        {
            var batches = await _batchRepository.FilterAsync(
                filter: b => (beginDate == null || b.ExpiresAtExtent.Date >= beginDate) &&
                             (endDate == null || b.ExpiresAtExtent.Date <= endDate) &&
                             (clientId == "all" || b.ClientId.ToString() == clientId),
                includeProperties: "Client.Contacts,CardType,RechargeType,BusinessArea",
                orderBy: bq => bq.OrderByDescending(b => b.Id));
            return batches.ProjectedAsCollection<BatchDTO>();
        }

        public async Task<List<CardBatchDTO>> GetBatchesByCardId(int cardId)
        {
            var card = (await _cardRepository.FilterAsync(
                filter: c => c.Id == cardId,
                includeProperties: "CardBatches.Batch"
            )).FirstOrDefault();

            if (card != null && card.CardBatches.Any())
            {
                var cardBatchDTOs= card.CardBatches.Where(cb => cb.Balance.Value > 0).ProjectedAsCollection<CardBatchDTO>();
                return cardBatchDTOs.OrderByDescending(c => c.BatchId).ToList();
            }
            return new List<CardBatchDTO>();
        }

        public async Task<List<CardBatchDTO>> GetBatchesByClientId(int clientId)
        {
            var batches = await _batchRepository.FilterAsync(filter: b => b.ClientId == clientId,
                                                             includeProperties: "CardBatches.Card.CardType,CardBatches.Card.Contact",
                                                             orderBy: b => b.OrderByDescending(b => b.CreatedAt));
            var cardBatches = new List<CardBatchDTO>();
            if (batches != null && batches.Any())
            {
                foreach (var batch in batches)
                {
                    cardBatches.AddRange(batch.CardBatches.Where(cb => cb.Balance.Value > 0).ProjectedAsCollection<CardBatchDTO>());
                }
            }
            return cardBatches.OrderByDescending(c => c.BatchId).ToList(); 
        }

        public async Task<List<BatchErrorDTO>> GetErrorBatches()
        {
            var batchErrors = await _batchErrorRepository.FilterAsync(orderBy: beq => beq.OrderByDescending(be => be.CreatedAt));
            return batchErrors.ProjectedAsCollection<BatchErrorDTO>();
        }

        public List<Load> GetLoadsFromFileStream(string fileName, Stream stream)
        {
            if (stream == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_BatchFileIsNull));
            }
            var loads = GetLoadsFromStream(stream);
            return loads;
        }

        public async Task<List<BatchErrorDTO>> PerformLoadsFromFileStreamAsync(string fileName, Stream stream)
        {
            if (stream == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_BatchFileIsNull));
            }
            var loads = GetLoadsFromStream(stream);
            var batchsErrors = new List<BatchError>();
            foreach (var load in loads)
            {
                BatchError batchError = null;
                try
                {
                    if (string.IsNullOrEmpty(load.ErrorMessage))
                        await PerformLoadAsync(load.Client, load.Contact, load.Card, load.Batch);
                    else
                    {
                        var errorMessage = $"Ocurrió un error al carga la fila {load.RowIndex} => {load.ErrorMessage}";
                        batchError = new BatchError(load.RowIndex, errorMessage, fileName, load.LineContent);
                    }
                }
                catch (Exception e)
                {
                    var errorMessage = $"Ocurrió un error al carga la fila {load.RowIndex} => {e.Message}";
                    batchError = new BatchError(load.RowIndex, errorMessage, fileName, load.LineContent);
                }
                if (batchError != null)
                {
                    _batchErrorRepository.Add(batchError);
                    batchsErrors.Add(batchError);
                }
            }
            await _batchErrorRepository.UnitOfWork.CommitAsync();
            return batchsErrors.ProjectedAsCollection<BatchErrorDTO>();
        }

        public async Task<BatchDTO> PerformLoadAsync(ClientListDTO clientDTO, ContactDTO contactDTO, CardDTO carDTO, BatchDTO batchDTO)
        {
            var client = (await _clientRepository.FilterAsync(filter: c => c.Ruc == clientDTO.Ruc && c.Status == Status.Active,
                                                              includeProperties: "Contacts.Cards")).FirstOrDefault();
            if (client == null)
            {
                client = new Client(clientDTO.Ruc, clientDTO.Address, clientDTO.Email, clientDTO.Name, clientDTO.Phone);
            }
            else
            {
                client.UpdateFields(clientDTO.Ruc, clientDTO.Address, clientDTO.Email, clientDTO.Name, clientDTO.Phone);
            }
            var mainContact = await _contactRepository.CreateOrUpdateMainContact(client, contactDTO.Phone, contactDTO.DocumentTypeId,
                                                                                 contactDTO.DocumentNumber, contactDTO.FirstName,
                                                                                 contactDTO.LastName, contactDTO.Email);
            var card = mainContact.Cards.FirstOrDefault(c => c.CardTypeId == carDTO.CardTypeId && c.Status == Status.Active);
            var cardType = _cardTypeRepository.Get(carDTO.CardTypeId);
            var batchReason = "Recarga de saldo";
            if (card == null)
            {
                batchReason = "Creación de tarjeta";
                card = new Card(mainContact, cardType.Currency, carDTO.CardTypeId, carDTO.TPCode);
            }
            batchDTO.TPContractReason += " " + batchReason;
            var expire = DateTime.Now.AddMonths(cardType.Term);
            var batchCurrency = _currencyRepository.Filter(c => c.Id == batchDTO.Amount.CurrencyId).FirstOrDefault();

            var batch = new Batch(
                mainContact,
                card,
                batchDTO.TPContractBatchNumber,
                new Money(batchCurrency, batchDTO.Amount.Value),
                expire,
                batchDTO.TPChasis,
                batchDTO.TPContractDate,
                batchDTO.TPInvoiceCode,
                batchDTO.TPInvoiceDate,
                batchDTO.RechargeTypeId,
                batchDTO.TPContractNumber,
                batchDTO.TPContractReason,
                batchDTO.DealerCode,
                batchDTO.DealerName,
                batchDTO.BusinessAreaId.Value,
                batchDTO.CardTypeId,
                batchDTO.LineContent
            );
            batch.BusinessArea = _businessAreaRepository.Filter(ba => ba.Id == batchDTO.BusinessAreaId).FirstOrDefault();
            client.Batches.Add(batch);
            _rechargeService.PerformRecharge(card, batch);

            if (client.Id == 0)
            {
                _clientRepository.Add(client);
            }
            else
            {
                _clientRepository.Modify(client);
            }
            await _clientRepository.UnitOfWork.CommitAsync();
            return batch.ProjectedAs<BatchDTO>();
        }

        public async Task ExtendExpiredDate(int id, DateTime newExpiredDate)
        {
            var batch = _batchRepository.Get(id);
            batch.ExpiresAtExtent = newExpiredDate;
            await _batchRepository.UnitOfWork.CommitAsync();
        }
        #endregion

        #region ConsoleApp 
        public async Task PerformBatchExpiration()
        {
            var cutDate = DateTime.Now;
            var batchesToExpire = _batchRepository.Filter( filter: (b) => b.Balance.Value > 0 && b.ExpiresAtExtent < cutDate);
            foreach(var batch in batchesToExpire)
            {
                var fullBatch = _batchRepository.Filter(
                   filter: (b) => b.Id == batch.Id,
                   includeProperties: "CardBatches.Balance.Currency,CardBatches.Card.Movements,BatchMovements,Balance.Currency,BusinessArea"
               ).FirstOrDefault();
                fullBatch.PerformExpiration();
                await _batchRepository.UnitOfWork.CommitAsync();
            }           
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _clientRepository.Dispose();
            _contactRepository.Dispose();
            _cardTypeRepository.Dispose();
            _cardRepository.Dispose();
            _batchRepository.Dispose();
            _batchErrorRepository.Dispose();
        }
        #endregion
    }
}
