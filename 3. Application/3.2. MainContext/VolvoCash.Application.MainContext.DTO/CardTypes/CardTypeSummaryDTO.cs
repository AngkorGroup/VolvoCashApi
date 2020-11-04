using VolvoCash.Application.MainContext.DTO.Common;

namespace VolvoCash.Application.MainContext.DTO.CardTypes
{
    public class CardTypeSummaryDTO
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public MoneyDTO Sum { get; set; }

        public string Color { get; set; }
        #endregion
    }
}
