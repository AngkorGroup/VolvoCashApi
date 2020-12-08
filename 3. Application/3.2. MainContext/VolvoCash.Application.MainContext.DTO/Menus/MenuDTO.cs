using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Menus
{
    public class MenuDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MenuType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        public string Icon { get; set; }

        public int? Order { get; set; }

        public int MenuParentId { get; set; }

        public string DisplayName { get; set; }
        #endregion
    }
}
