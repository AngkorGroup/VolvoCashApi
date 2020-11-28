using VolvoCash.Domain.Seedwork;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using VolvoCash.Application.Seedwork.DateConverters;

namespace VolvoCash.Application.MainContext.DTO.Users
{
    public class UserDTO
    {
        #region Properties
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UserType Type { get; set; }

        public CashierDTO Cashier { get; set; }

        public AdminDTO Admin { get; set; }

        public ContactListDTO Contact { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
