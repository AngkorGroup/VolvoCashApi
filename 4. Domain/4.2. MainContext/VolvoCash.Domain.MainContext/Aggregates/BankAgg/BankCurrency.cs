using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BankAgg
{
    public class BankCurrency : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public string Equivalence { get; set; }

        [Required]
        [ForeignKey("Bank")]
        public int BankId { get; set; }

        public Bank Bank { get; set; }

        [Required]
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }
        #endregion
    }
}
