using System;
using Newtonsoft.Json;
using VolvoCash.Application.MainContext.DTO.BusinessAreas;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.RechargeTypes;
using VolvoCash.Application.Seedwork.DateConverters;

namespace VolvoCash.Application.MainContext.DTO.Batches
{
    public class BatchDTO
    {
        #region Properties
        public int Id { get; set; }

        public MoneyDTO Amount { get; set; }

        public MoneyDTO Balance { get; set; }

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

        public int RechargeTypeId { get; set; }

        public RechargeTypeDTO RechargeType { get; set; }

        public int ClientId { get; set; }

        public ClientListDTO Client { get; set; }

        public string TPContractNumber { get; set; }

        public string TPContractBatchNumber { get; set; }

        public string TPContractReason { get; set; }

        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        public int? BusinessAreaId { get; set; }

        public BusinessAreaDTO BusinessArea { get; set; }

        public int CardTypeId { get; set; }

        public CardTypeDTO CardType { get; set; }

        public string LineContent { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
        #endregion
    }
}
