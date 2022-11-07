using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;
namespace VolvoCash.Application.MainContext.DTO.Mappings
{
    public class MappingDetailDTO
    {
        #region Properties    
        public int Id { get; set; }
       
        public string Type { get; set; }
       
        public string DocumentType { get; set; }
       
        public string Line { get; set; }
       
        public string RecordType { get; set; }
       
        public string Company { get; set; }
       
        public string Reference { get; set; }
       
        public string PostKey { get; set; }
       
        public string Account { get; set; }
       
        public string Sign { get; set; }
       
        public string TaxCode { get; set; }
       
        public string TaxAmount { get; set; }
       
        public string CostCenter { get; set; }
       
        public string ProfitCenter { get; set; }
       
        public string TradePartner { get; set; }
       
        public string DocText { get; set; }
       
        public string MoreInfo { get; set; }
       
        public string BusinessArea { get; set; }
       
        public string Market { get; set; }
       
        public string Customer { get; set; }
       
        public string ProductModel { get; set; }
       
        public string LineType { get; set; }
       
        public string Classification { get; set; }

        public int MappingHeaderId { get; set; }

        public MappingHeaderDTO MappingHeader { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
