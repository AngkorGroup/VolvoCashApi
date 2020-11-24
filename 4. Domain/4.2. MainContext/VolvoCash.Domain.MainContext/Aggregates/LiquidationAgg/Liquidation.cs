using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg
{
    public class Liquidation : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public Money Amount { get; set; }

        [ForeignKey("Dealer")]
        public int DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }

        public LiquidationStatus LiquidationStatus { get; set; }

        public virtual ICollection<Charge> Charges { get; set; } = new List<Charge>();
        #endregion

        #region Constructor
        public Liquidation()
        {
        }

        public Liquidation(Money amount, int dealerId)
        {
            Amount = amount;
            DealerId = dealerId;
            LiquidationStatus = LiquidationStatus.Generated;
        }
        #endregion
    }
}
