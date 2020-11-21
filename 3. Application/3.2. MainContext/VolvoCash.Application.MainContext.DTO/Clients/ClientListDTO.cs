using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Status;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Aggregates.StatusAgg;

namespace VolvoCash.Application.MainContext.DTO.Clients
{
    public class ClientListDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ruc { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusDTO Status { get; set; }

        [JsonConverter(typeof(DefaultShortLiterallyDateConverter))]
        public DateTime CreatedAt { get; set; }

        public ContactListDTO MainContact { get; set; }
        #endregion
    }
}
