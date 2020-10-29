using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.CardTypes
{
    public class CardTypeDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }
        #endregion
    }
}
