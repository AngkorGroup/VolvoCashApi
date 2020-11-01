namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class CardTypeSummary
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Money Sum { get; set; }
        #endregion
    }
}
