using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Status;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Aggregates.StatusAgg;

namespace VolvoCash.Application.MainContext.DTO.Clients
{
    public class ClientFilterDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ruc { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusDTO Status { get; set; }

        public MoneyDTO Balance { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
