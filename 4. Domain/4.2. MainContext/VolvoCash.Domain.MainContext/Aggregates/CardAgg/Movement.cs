using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class Movement : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public Money Amount { get; set; }

        [Required]
        public MovementType Type { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Card")]
        public int CardId { get; set; }

        public virtual Card Card { get; set; }

        public virtual ICollection<BatchMovement> BatchMovements { get; set; }

        [ForeignKey("Charge")]
        public int? ChargeId { get; set; }

        public virtual Charge Charge { get; set; }

        [ForeignKey("Transfer")]
        public int? TransferId { get; set; }

        public virtual Transfer Transfer { get; set; }
        #endregion

        #region Constructor
        public Movement()
        {

        }

        public Movement(Money amount, string description, string displayName, MovementType movementType,
                        ICollection<BatchMovement> batchMovements = null,Transfer transfer = null )
        {
            Amount = amount;
            Description = description;
            DisplayName = displayName;
            BatchMovements = batchMovements;
            Type = movementType;
            Transfer = transfer;
        }
        #endregion
    }
}
