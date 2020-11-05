using Newtonsoft.Json;
using System;
using VolvoCash.Domain.Seedwork;
using VolvoCash.Application.MainContext.DTO.Dealers;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;
using Newtonsoft.Json.Converters;

namespace VolvoCash.Application.MainContext.DTO.Cashiers
{
    public class CashierDTO : AuditableEntity
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public string Imei { get; set; }

        public string TPCode { get; set; }

        public int DealerId { get; set; }

        public virtual DealerDTO Dealer { get; set; }

        public int UserId { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public new DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }
        

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }       
        #endregion
    }
}
