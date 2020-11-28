using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class Transfer : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(20)]
        public string OperationCode { get; set; }

        [Required]
        public Money Amount { get; set; }

        [MaxLength(100)]
        public string DisplayName { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey("OriginCard")]
        public int OriginCardId { get; set; }

        public virtual Card OriginCard { get; set; }

        [Required]
        [ForeignKey("DestinyCard")]
        public int DestinyCardId { get; set; }

        public virtual Card DestinyCard { get; set; }

        public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
        #endregion

        #region Constructor
        public Transfer()
        {
        }

        public Transfer(Card originCard, Card destinyCard, Money amount, string displayName)
        {
            DisplayName = displayName?.Substring(0, Math.Min(100, displayName.Length));
            OriginCard = originCard;
            DestinyCard = destinyCard;
            Amount = amount;
            DisplayName = displayName;
            GenerateOperationCode();
        }
        #endregion

        #region Public Methods
        public void GenerateOperationCode()
        {
            OperationCode = RandomGenerator.RandomDigits(10);
        }
        #endregion
    }
}
