using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class CardTypeSummary: Entity
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Money Sum { get; set; }
        #endregion
    }
}
