using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.POJOS;
using VolvoCash.Application.MainContext.Batches.Services;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Services.CardService;

namespace VolvoCash.Application.MainContext.Cards.Services
{
    public class BatchAppService : IBatchAppService
    {

        #region Members
        private readonly IClientRepository _clientRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ICardTypeRepository _cardTypeRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IBatchErrorRepository _batchErrorRepository;
        private readonly ICardRechargeService _rechargeService;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public BatchAppService(IClientRepository clientRepository,
                               IContactRepository contactRepository,
                               ICardTypeRepository cardTypeRepository,
                               IBatchRepository batchRepository,
                               IBatchErrorRepository batchErrorRepository,
                               ICardRechargeService rechargeService)
        {
            _clientRepository = clientRepository;
            _contactRepository = contactRepository;
            _cardTypeRepository = cardTypeRepository;
            _batchRepository = batchRepository;
            _batchErrorRepository = batchErrorRepository;
            _rechargeService = rechargeService;
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Private Methods
        public IEnumerable<string> ReadLines(Stream stream, Encoding encoding)
        {
            using (var reader = new StreamReader(stream, encoding))
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
                var contractDate = DateTime.ParseExact(GetLineSegment(lineSegments, 4), DateTimeFormats.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                var chasisNumber = GetLineSegment(lineSegments, 5);
                var invoiceDocumentNumber = GetLineSegment(lineSegments, 6);
                var invoiceDate = DateTime.ParseExact(GetLineSegment(lineSegments, 7), DateTimeFormats.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                var batchAmount = double.Parse(GetLineSegment(lineSegments, 8));
                var batchCurrency = GetLineSegment(lineSegments, 9);
                var contactName = GetLineSegment(lineSegments, 10);
                var contractType = GetLineSegment(lineSegments, 11) == "A" ? TPContractType.Addendum : TPContractType.Contract;
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
                var clientAddress = "Mi direccion 1234";
                var clientPhone = "987654321";
                var contactLastName = "Apellidos";
                var contactDocumentType = DocumentType.DNI;
                try
                {
                    var client = new ClientDTO()
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
                        DocumentType = contactDocumentType,
                        DocumentNumber = contactDocumentNumber,
                    };

                    var cardType = _cardTypeRepository.Filter(ct => ct.TPCode == cardTypeCode).FirstOrDefault();

                    if (cardType == null)
                        throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_InvalidCardTypeCode));

                    var card = new CardDTO()
                    {
                        CardTypeId = cardType.Id,
                        TPCode = batchTPCode,
                    };

                    var batch = new BatchDTO()
                    {
                        Amount = new MoneyDTO((Currency)Enum.Parse(typeof(Currency), batchCurrency), batchAmount),
                        TPContractDate = contractDate,
                        TPChasis = chasisNumber,
                        TPInvoiceDate = invoiceDate,
                        TPInvoiceCode = invoiceDocumentNumber,
                        TPContractType = contractType,
                        TPContractNumber = contractNumber,
                        TPContractBatchNumber = batchTPCode,
                        DealerCode = dealerCode,
                        DealerName = dealerName,
                        BusinessCode = businessCode,
                        BusinessDescription = businessDescription,
                        CardTypeId = cardType.Id,
                        LineContent = line
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
            foreach (var line in ReadLines(stream, Encoding.UTF8))
            {
                loads.Add(GetLoadFromLine(line, rowIndex));
                rowIndex++;
            }
            return loads;
        }
        #endregion

        #region ApiWeb Public Methods
        public async Task<List<BatchDTO>> GetBatches()
        {
            var batches = await _batchRepository.FilterAsync(includeProperties: "Client.Contacts,CardType",
                                                             orderBy: bq => bq.OrderByDescending(b => b.CreatedAt));
            return batches.ProjectedAsCollection<BatchDTO>();
        }

        public async Task<List<BatchErrorDTO>> GetErrorBatches()
        {
            var batchErrors = await _batchErrorRepository.FilterAsync(orderBy: beq => beq.OrderByDescending(be => be.CreatedAt));
            return batchErrors.ProjectedAsCollection<BatchErrorDTO>();
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

        public async Task<BatchDTO> PerformLoadAsync(ClientDTO clientDTO, ContactDTO contactDTO, CardDTO carDTO, BatchDTO batchDTO)
        {
            var client = (await _clientRepository.FilterAsync(filter: c => c.Ruc == clientDTO.Ruc,
                                                              includeProperties: "Contacts.Cards")).FirstOrDefault();
            if (client == null)
            {
                client = new Client(clientDTO.Ruc, clientDTO.Address, clientDTO.Email, clientDTO.Name, clientDTO.Phone);
            }
            else
            {
                client.UpdateFields(clientDTO.Ruc, clientDTO.Address, clientDTO.Email, clientDTO.Name, clientDTO.Phone);
            }
            var mainContact = await _contactRepository.CreateOrUpdateMainContact(client, contactDTO.Phone, contactDTO.DocumentType,
                                                                                 contactDTO.DocumentNumber, contactDTO.FirstName,
                                                                                 contactDTO.LastName, contactDTO.Email);
            var card = mainContact.Cards.FirstOrDefault(c => c.CardTypeId == carDTO.CardTypeId);
            var cardType = _cardTypeRepository.Get(carDTO.CardTypeId);
            var batchReason = "Recarga de saldo";
            if (card == null)
            {
                batchReason = "Creación de tarjeta";
                card = new Card(mainContact, cardType.Currency, carDTO.CardTypeId, carDTO.TPCode);
            }
            batchDTO.TPContractReason += " " + batchReason;
            var expire = DateTime.Now.AddDays(cardType.Term);
            var batch = new Batch(
                batchDTO.TPContractBatchNumber,
                new Money(batchDTO.Amount.Currency, batchDTO.Amount.Value),
                expire,
                batchDTO.TPChasis,
                batchDTO.TPContractDate,
                batchDTO.TPInvoiceCode,
                batchDTO.TPInvoiceDate,
                batchDTO.TPContractType,
                batchDTO.TPContractNumber,
                batchDTO.TPContractReason,
                batchDTO.DealerCode,
                batchDTO.DealerName,
                batchDTO.BusinessCode,
                batchDTO.BusinessDescription,
                batchDTO.CardTypeId,
                batchDTO.LineContent
            );
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
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _clientRepository.Dispose();
            _contactRepository.Dispose();
            _cardTypeRepository.Dispose();
        }
        #endregion
    }
}
