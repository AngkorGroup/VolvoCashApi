using System;
using VolvoCash.Domain.Seedwork;
using System.Text.Json.Serialization;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;
using Newtonsoft.Json.Converters;

namespace VolvoCash.Application.MainContext.DTO.Dealers
{
    public class DealerDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Ruc { get; set; }

        public string ContactName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DealerType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        public string Zone { get; set; }

        public int MaxCashiers { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string TPCode { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }

        #endregion
    }
}
