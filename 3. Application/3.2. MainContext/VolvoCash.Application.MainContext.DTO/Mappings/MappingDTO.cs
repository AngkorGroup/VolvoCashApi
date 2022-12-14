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
       
        public string Filler { get; set; }
       
        public string Version { get; set; }
       
        public string ReceiverLogicalID { get; set; }
       
        public string ReceiverComponentID { get; set; }
       
        public string SenderLogicalID { get; set; }
       
        public string SenderComponentID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
