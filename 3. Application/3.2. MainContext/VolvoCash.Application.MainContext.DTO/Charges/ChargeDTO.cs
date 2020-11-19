using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.EnumAgg;

namespace VolvoCash.Application.MainContext.DTO.Charges
{
    public class ChargeDTO
    {
        #region Properties
        public int Id { get; set; }

        public string OperationCode { get; set; }

        public MoneyDTO Amount { get; set; }

        public string DisplayName { get; set; }

        public string ImageUrl { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ChargeStatus Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ChargeType ChargeType { get; set; }

        public int CardId { get; set; }

        public int CashierId { get; set; }

        public CashierDTO Cashier { get; set; }

        [JsonConverter(typeof(DefaultLiterallyDateConverter))]
        public DateTime CreatedAt { get; set; }

        public string CardToken { get; set; }

        public string Description { get; set; }

        public CardListDTO Card { get; set; }
        #endregion
    }
}
