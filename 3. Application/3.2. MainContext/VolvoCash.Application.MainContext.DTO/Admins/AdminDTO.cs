using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Application.MainContext.DTO.Roles;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.Admins
{
    public class AdminDTO : AuditableEntity
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }

        public int? DealerId { get; set; }

        public int? CashierId { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public new DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? ArchiveAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        public List<RoleAdminDTO> RoleAdmins { get; set; }

        public List<int> RoleIds { get; set; }

        public List<string> MenuOptions { get; set; }
        #endregion
    }
}
