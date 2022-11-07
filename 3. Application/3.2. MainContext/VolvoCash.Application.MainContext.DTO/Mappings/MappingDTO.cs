using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;
namespace VolvoCash.Application.MainContext.DTO.Mappings
{
    public class MappingDTO
    {
        #region Properties       
        public int Id { get; set; }

        public string MappingNumber { get; set; }
       
        public string Name { get; set; }
       
        public string Type { get; set; }
       
        public string Description { get; set; }
       
        public string Company { get; set; }
        
        public string Feeder { get; set; }
       
        public string File { get; set; }
       
        public string Username { get; set; }
       
        public string Password { get; set; }
       
        public string Date { get; set; }
       
        public string Filter { get; set; }
       
        public string Version { get; set; }
       
        public string ReceiverLogicalId { get; set; }
       
        public string ReceiverComponentId { get; set; }
       
        public string SenderLogicalId { get; set; }
       
        public string SenderComponentId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
