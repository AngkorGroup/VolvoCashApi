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
    }
}
