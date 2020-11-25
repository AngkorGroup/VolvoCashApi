using VolvoCash.Application.MainContext.DTO.Currencies;

namespace VolvoCash.Application.MainContext.DTO.Common
{
    public class MoneyDTO
    {
        #region Properties
        public double Value { get; set; }

        public int CurrencyId { get; set; }

        public CurrencyDTO Currency { get; set; }

        public string CurrencyLabel { get => Currency?.Name ?? (CurrencyId == 1 ? "Dólares" : "Soles" ); }

        public string CurrencySymbol { get => Currency?.Symbol ?? (CurrencyId == 1 ? "US$" : "S/."); }

        public string CurrencyCode { get => Currency?.TPCode ?? (CurrencyId == 1 ? "USD" : "PEN"); }

        public string Label { get => CurrencySymbol + " " + string.Format("{0:#,0.00}", Value); }
        #endregion

        #region Constructor
        public MoneyDTO()
        {
        }
        #endregion
    }
}
