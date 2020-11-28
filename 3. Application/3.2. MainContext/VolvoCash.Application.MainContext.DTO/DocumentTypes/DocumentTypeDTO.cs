using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Application.Seedwork.DateConverters;

namespace VolvoCash.Application.MainContext.DTO.DocumentTypes
{
    public class DocumentTypeDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string TPCode { get; set; }

        public string Abbreviation { get; set; }

        public string SUNATCode { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        #endregion
    }
}
