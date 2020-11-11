using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Common
{
    public class MoneyDTO
    {
        #region Properties
        public double Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public string CurrencyLabel { get => Currency == Currency.PEN ? "Soles" : "Dólares" ; }

        public string CurrencySymbol { get => Currency == Currency.PEN ? "S/." : "US$"; }

        public string Label { get => CurrencySymbol + " " + string.Format("{0:#,0.00}", Value); }
        #endregion

        #region Constructor
        public MoneyDTO(Currency currency, double value)
        {
            Currency = currency;
            Value = value;
        }
        #endregion
    }
}
