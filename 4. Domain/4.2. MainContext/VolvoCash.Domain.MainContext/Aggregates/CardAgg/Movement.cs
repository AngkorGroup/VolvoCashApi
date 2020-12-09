using System;
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

        [MaxLength(100)]
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

        #region Not Mapped Properties
        [NotMapped]
        public int? BatchId { get; set; }
        #endregion

        #region Constructor
        public Movement()
        {

        }

        public Movement(Money amount, string description, string displayName, MovementType movementType, Charge charge)
        {
            Amount = amount;
            Description = description?.Substring(0, Math.Min(200, description.Length));
            DisplayName = displayName?.Substring(0, Math.Min(100, displayName.Length));
            BatchMovements = new List<BatchMovement>();
            Type = movementType;
            Charge = charge;
        }

        public Movement(Money amount, string description, string displayName, MovementType movementType)
        {
            Amount = amount;
            Description = description?.Substring(0, Math.Min(200, description.Length));
            DisplayName = displayName?.Substring(0, Math.Min(100, displayName.Length));
            BatchMovements = new List<BatchMovement>();
            Type = movementType;
        }

        public Movement(Money amount, string description, string displayName, MovementType movementType, ICollection<BatchMovement> batchMovements)
        {
            Amount = amount;
            Description = description?.Substring(0, Math.Min(200, description.Length));
            DisplayName = displayName?.Substring(0, Math.Min(100, displayName.Length));
            BatchMovements = batchMovements;
            Type = movementType;
        }

        public Movement(Money amount, string description, string displayName, MovementType movementType, ICollection<BatchMovement> batchMovements,Transfer transfer)
        {
            Amount = amount;
            Description = description?.Substring(0, Math.Min(200, description.Length));
            DisplayName = displayName?.Substring(0, Math.Min(100, displayName.Length));
            BatchMovements = batchMovements;
            Type = movementType;
            Transfer = transfer;
        }
        #endregion
    }
}
