using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Mappings
{
    public class MappingHeaderDTO
    {
        #region Properties    
        public int Id { get; set; }

        public string Type { get; set; }
       
        public string RecordType { get; set; }
       
        public string Company { get; set; }
       
        public string DocumentNumber { get; set; }
       
        public string Reference { get; set; }
       
        public string Control { get; set; }
       
        public string DocumentType { get; set; }
       
        public string DocumentDate { get; set; }
       
        public string PostDate { get; set; }
       
        public string Currency { get; set; }
       
        public string ExchangeRate { get; set; }
       
        public string DocumentHeader { get; set; }
       
        public string TranslationDate { get; set; }
       
        public string IntercompanyNumber { get; set; }
       
        public string TradingPartner { get; set; }
       
        public string ExchangeRateType { get; set; }
       
        public string PostingPeriod { get; set; }
       
        public string ExchangeRateToFactor { get; set; }
       
        public string ExchangeRateFromFactor { get; set; }
       
        public string ReversalReason { get; set; }
       
        public string ReversalDate { get; set; }

        public int MappingId { get; set; }

        public MappingDTO Mapping { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
