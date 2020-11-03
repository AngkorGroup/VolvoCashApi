using System;
using VolvoCash.Domain.Seedwork;
using System.Text.Json.Serialization;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Dealers
{
    public class DealerDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Ruc { get; set; }

        public string ContactName { get; set; }

        public DealerType Type { get; set; }

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
