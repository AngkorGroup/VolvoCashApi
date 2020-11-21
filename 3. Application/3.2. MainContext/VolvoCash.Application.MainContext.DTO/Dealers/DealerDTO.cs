using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Application.MainContext.DTO.Status;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.StatusAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.Dealers
{
    public class DealerDTO : AuditableEntity
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ruc { get; set; }

        public string ContactName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DealerType Type { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusDTO Status { get; set; }

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
