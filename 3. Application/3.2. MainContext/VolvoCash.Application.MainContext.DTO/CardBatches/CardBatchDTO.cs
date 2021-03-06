﻿using System;
using Newtonsoft.Json;
using VolvoCash.Application.MainContext.DTO.Batches;
using VolvoCash.Application.MainContext.DTO.Cards;
using VolvoCash.Application.MainContext.DTO.Common;
using VolvoCash.Application.Seedwork.DateConverters;

namespace VolvoCash.Application.MainContext.DTO.CardBatches
{
    public class CardBatchDTO
    {
        #region Properties
        public int Id { get; set; }

        public MoneyDTO Balance { get; set; }

        public int BatchId { get; set; }

        public virtual BatchListDTO Batch { get; set; }

        public int CardId { get; set; }

        public virtual CardListDTO Card { get; set; }

        [JsonConverter(typeof(DefaultShortLiterallyDateConverter))]
        public DateTime ExpiresAt { get; set; }

        [JsonConverter(typeof(DefaultShortLiterallyDateConverter))]
        public DateTime ExpiresAtExtent { get;  set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
