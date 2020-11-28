using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

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

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultShortLiterallyDateConverter))]
        public DateTime CreatedAt { get; set; }

        public ContactListDTO MainContact { get; set; }
        public string Email { get; set; }
        public string TPCode { get; set; }
        #endregion
    }
}
