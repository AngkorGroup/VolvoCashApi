﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Application.MainContext.DTO.Clients;
using VolvoCash.Application.MainContext.DTO.DocumentTypes;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Contacts
{
    public class ContactListDTO
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

        public int DocumentTypeId { get; set; }

        public DocumentTypeDTO DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

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
