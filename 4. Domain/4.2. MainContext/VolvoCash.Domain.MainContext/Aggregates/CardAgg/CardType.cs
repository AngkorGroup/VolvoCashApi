using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.MainContext.Enums;
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

        [ForeignKey("Currency")]
        public int? CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

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
