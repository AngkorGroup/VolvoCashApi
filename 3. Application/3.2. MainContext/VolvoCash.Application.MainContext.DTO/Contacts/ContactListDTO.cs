using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.Status;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;
using VolvoCash.Domain.MainContext.Aggregates.StatusAgg;

namespace VolvoCash.Application.MainContext.DTO.Contacts
{
    public class ContactListDTO
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public ContactType Type { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentType DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusDTO Status { get; set; }

        public ClientListDTO Client { get; set; }

        public int ClientId { get; set; }

        public int UserId { get; set; }

        [JsonConverter(typeof(DefaultShortLiterallyDateConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
