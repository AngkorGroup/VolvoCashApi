using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class CardBatch : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public Money Balance { get; set; }

        [Required]
        [ForeignKey("Card")]
        public int CardId { get; set; }

        public virtual Card Card { get; set; }

        [Required]
        [ForeignKey("Batch")]
        public int BatchId { get; set; }

        public virtual Batch Batch { get; set; }
        #endregion

        #region Constructor
        public CardBatch()
        {
        }

        public CardBatch(int batchId, int cardId, Money amount)
        {
            BatchId = batchId;
            CardId = cardId;
            Balance = amount;
        }

        public CardBatch(Batch batch, Card card, Money amount)
        {
            Batch = batch;
            Card = card;
            Balance = amount;
        }
        #endregion         

        #region Public Methods
        public void SubstractToBalance(Money amount)
        {
            Balance = Balance.Substract(amount);
            Batch.Balance = Batch.Balance.Substract(amount);
        }

        public void AddToBalance(Money amount)
        {
            Balance = Balance.Add(amount);
            Batch.Balance = Batch.Balance.Add(amount);
        }
        #endregion
    }
}
