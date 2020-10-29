using System;
using Newtonsoft.Json;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.Seedwork.DateConverters;

namespace VolvoCash.Application.MainContext.DTO.Transfers
{
    public class TransferListDTO
    {
        #region Properties
        public int Id { get; set; }

        public string OperationCode { get; set; }

        public MoneyDTO Amount { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int OriginCardId { get; set; }

        public CardListDTO OriginCard { get; set; }

        public int DestinyCardId { get; set; }

        public CardListDTO DestinyCard { get; set; }

        [JsonConverter(typeof(DefaultLiterallyDateConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
