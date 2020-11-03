using Newtonsoft.Json;
using System;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.Seedwork.DateConverters;

namespace VolvoCash.Application.MainContext.DTO.Clients
{
    public class ClientDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ruc { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public ContactDTO MainContact { get; set; }

        public string Email { get; set; }

        public string TPCode { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
