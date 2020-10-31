using Newtonsoft.Json;
using System;
using VolvoCash.Application.Seedwork.DateConverters;

namespace VolvoCash.Application.MainContext.DTO.Batches
{
    public class BatchErrorDTO
    {
        #region Properties
        public int RowIndex { get; set; }

        public string ErrorMessage { get; set; }

        public string FileName { get; set; }

        public string LineContent { get; set; }

        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
