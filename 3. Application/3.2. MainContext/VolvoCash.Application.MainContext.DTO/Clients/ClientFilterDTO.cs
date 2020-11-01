using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Clients
{
    public class ClientFilterDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ruc { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        public Money Balance { get; set; }
        #endregion
    }
}
