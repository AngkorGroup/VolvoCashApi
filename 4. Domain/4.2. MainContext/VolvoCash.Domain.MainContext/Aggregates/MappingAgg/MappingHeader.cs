using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.MappingAgg
{
    public class MappingHeader : AuditableEntityWithKey<int>
    {
        #region Properties
        [MaxLength(200)]
        public string Type { get; set; }

        [MaxLength(200)]
        public string RecordType { get; set; }

        [MaxLength(200)]
        public string Company { get; set; }

        [MaxLength(200)]
        public string DocumentNumber { get; set; }

        [MaxLength(200)]
        public string Reference { get; set; }

        [MaxLength(200)]
        public string Control { get; set; }

        [MaxLength(200)]
        public string DocumentType { get; set; }

        [MaxLength(200)]
        public string DocumentDate { get; set; }

        [MaxLength(200)]
        public string PostDate { get; set; }

        [MaxLength(200)]
        public string Currency { get; set; }

        [MaxLength(200)]
        public string ExchangeRate { get; set; }

        [MaxLength(200)]
        public string DocumentHeader { get; set; }

        [MaxLength(200)]
        public string TranslationDate { get; set; }

        [MaxLength(200)]
        public string IntercompanyNumber { get; set; }

        [MaxLength(200)]
        public string TradingPartner { get; set; }

        [MaxLength(200)]
        public string ExchangeRateType { get; set; }

        [MaxLength(200)]
        public string PostingPeriod { get; set; }

        [MaxLength(200)]
        public string ExchangeRateToFactor { get; set; }

        [MaxLength(200)]
        public string ExchangeRateFromFactor { get; set; }

        [MaxLength(200)]
        public string ReversalReason { get; set; }

        [MaxLength(200)]
        public string ReversalDate { get; set; }

        [ForeignKey("Mapping")]
        public int MappingId { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }

        public virtual Mapping Mapping { get; set; }

        public virtual ICollection<MappingDetail> MappingDetails { get; set; } = new List<MappingDetail>();
        #endregion

        #region NotMapped Properties
        #endregion

        #region Constructor
        public MappingHeader()
        {
        }

        public MappingHeader(string type, string recordType, string company, string documentNumber, string reference, string control,
                            string documentType, string documentDate, string postDate, string currency, string exchangeRate,
                            string documentHeader, string translationDate, string intercompanyNumber, string tradingPartner,
                            string exchangeRateType, string postingPeriod, string exchangeRateToFactor, string exchangeRateFromFactor,
                            string reversalReason, string reversalDate, int mappingId)
        {
            Type = type;
            RecordType = recordType;
            Company = company;
            DocumentNumber = documentNumber;
            Reference = reference;
            Control = control;
            DocumentType = documentType;
            DocumentDate = documentDate;
            PostDate = postDate;
            Currency = currency;
            ExchangeRate = exchangeRate;
            DocumentHeader = documentHeader;
            TranslationDate = translationDate;
            IntercompanyNumber = intercompanyNumber;
            TradingPartner = tradingPartner;
            ExchangeRateType = exchangeRateType;
            PostingPeriod = postingPeriod;
            ExchangeRateToFactor = exchangeRateToFactor;
            ExchangeRateFromFactor = exchangeRateFromFactor;
            ReversalReason = reversalReason;
            ReversalDate = reversalDate;
            MappingId = mappingId;
            Status = Status.Active;
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
