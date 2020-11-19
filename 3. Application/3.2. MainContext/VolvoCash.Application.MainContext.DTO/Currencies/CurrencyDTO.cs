using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.Currencies
{
    public class CurrencyDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }
        public char Symbol { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
