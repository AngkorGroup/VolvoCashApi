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

        #region Constructor
        public BatchMovement()
        {

        }

        public BatchMovement(Money amount, int batchId)
        {
            Amount = amount;
            BatchId = batchId;
        }

        public BatchMovement(Money amount, Batch batch)
        {
            Amount = amount;
            Batch = batch;
        }

        public BatchMovement(Money amount, Batch batch,Movement movement)
        {
            Amount = amount;
            Batch = batch;
            BatchId = batch.Id;
            Movement = movement;
        }
        #endregion
    }
}
