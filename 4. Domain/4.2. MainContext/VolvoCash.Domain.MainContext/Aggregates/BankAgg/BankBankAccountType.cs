using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BankAgg
{
    public class BankBankAccountType : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public string Equivalence { get; set; }

        [Required]
        [ForeignKey("Bank")]
        public int BankId { get; set; }

        public Bank Bank { get; set; }

        [Required]
        [ForeignKey("BankAccountType")]
        public int BankAccountTypeId { get; set; }

        public BankAccountType BankAccountType { get; set; }
        #endregion
    }
}
