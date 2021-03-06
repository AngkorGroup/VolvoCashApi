using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.MainContext.DTO.Currencies;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.CardTypes
{
    public class CardTypeDTO : AuditableEntity
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int Term { get; set; }

        public int CurrencyId { get; set; }
        
        public CurrencyDTO Currency { get; set; }

        public string TPCode { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public new DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
