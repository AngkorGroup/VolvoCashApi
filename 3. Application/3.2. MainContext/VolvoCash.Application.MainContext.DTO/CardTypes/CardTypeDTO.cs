using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;
using VolvoCash.Domain.Seedwork;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.CardTypes
{
    public class CardTypeDTO :  AuditableEntity
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
