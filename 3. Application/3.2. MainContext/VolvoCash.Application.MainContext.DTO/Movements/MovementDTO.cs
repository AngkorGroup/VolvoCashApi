using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Application.MainContext.DTO.Charges;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Transfers;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Movements
{
    public class MovementDTO
    {
        #region Properties
        public int Id { get; set; }

        public MoneyDTO Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MovementType Type { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public ChargeListDTO Charge { get; set; }

        public TransferListDTO Transfer { get; set; }

        [JsonConverter(typeof(DefaultLiterallyDateConverter))]
        public DateTime CreatedAt { get; set; }

        public int? BatchId { get; set; }
        #endregion
    }
}
