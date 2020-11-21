using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.Status;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;

namespace VolvoCash.Application.MainContext.DTO.Contacts
{
    public class ContactDTO
    {
        #region Properties
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ContactType Type { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentType DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusDTO Status { get; set; }

        public int ClientId { get; set; }

        public List<ContactListDTO> ContactChildren { get; set; }

        public int? ContactParentId { get; set; }

        public ContactListDTO ContactParent { get; set; }

        public int UserId { get; set; }

        [JsonConverter(typeof(DefaultShortLiterallyDateConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
