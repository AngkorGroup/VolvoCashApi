using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BatchAgg
{
    public class BatchMovement : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public Money Amount { get; set; }

        [Required]
        [ForeignKey("Batch")]
        public int BatchId { get; set; }

        public Batch Batch { get; set; }

        [Required]
        [ForeignKey("Movement")]
        public int MovementId { get; set; }

        public Movement Movement { get; set; }
        #endregion
    }
}
