using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Batches
{
    public class BatchDTO
    {
        #region Properties
        public int Id { get; set; }

        public MoneyDTO Amount { get; set; }

        [JsonConverter(typeof(DefaultDateConverter))]
        public DateTime ExpiresAt { get; set; }

        [JsonConverter(typeof(DefaultDateConverter))]
        public DateTime ExpiresAtExtent { get; set; }

        [JsonConverter(typeof(DefaultDateConverter))]
        public DateTime? TPContractDate { get; set; }

        public string TPChasis { get; set; }

        [JsonConverter(typeof(DefaultDateConverter))]
        public DateTime? TPInvoiceDate { get; set; }

        public string TPInvoiceCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TPContractType TPContractType { get; set; }

        public int ClientId { get; set; }

        public ClientDTO Client { get; set; }

        public string TPContractNumber { get; set; }

        public string TPContractBatchNumber { get; set; }

        public string TPContractReason { get; set; }

        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        public string BusinessCode { get; set; }

        public string BusinessDescription { get; set; }
        #endregion
    }
}
