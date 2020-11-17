using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.EnumAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class CardType : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string DisplayName { get; set; }

        [Required]
        public int Term { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        [MaxLength(10)]
        public string Color { get; set; }

        public string ImageUrl { get; set; }

        [MaxLength(100)]
        public string TPCode { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        #endregion
    }
}
