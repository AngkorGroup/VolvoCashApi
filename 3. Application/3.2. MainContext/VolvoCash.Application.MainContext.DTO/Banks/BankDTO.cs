using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Banks
{
    public class BankDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string TPCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
