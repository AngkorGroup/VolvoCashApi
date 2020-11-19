using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using VolvoCash.Application.MainContext.DTO.CardBatches;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Movements;
using VolvoCash.Application.Seedwork.DateConverters;
using VolvoCash.Domain.MainContext.EnumAgg;

namespace VolvoCash.Application.MainContext.DTO.Cards
{
    public class CardDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Code { get; set; }

        public MoneyDTO Balance { get; set; }

        public MoneyDTO CalculatedBalance { get; set; }

        public string QrUrl { get; set; }

        public ContactListDTO Contact { get; set; }

        public int ContactId { get; set; }

        public List<MovementDTO> Movements { get; set; }

        public List<CardBatchDTO> CardBatchesWithBalance { get; set; }

        public int CardTypeId { get; set; }

        public CardTypeDTO CardType { get; set; }

        public string TPCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion

        #region Public Methods
        public dynamic GetCardForQr()
        {
            return new
            {
                Code,
                Id
            };
        }
        #endregion
    }
}
