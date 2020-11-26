using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg
{
    public class BankAccount : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(20)]
        public string Account { get; set; }

        [MaxLength(40)]
        public string CCI { get; set; }

        [Required]
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public bool IsDefault { get; set; }

        [Required]
        [ForeignKey("BankAccountType")]
        public int BankAccountTypeId { get; set; }

        public virtual BankAccountType BankAccountType { get; set; }

        [Required]
        [ForeignKey("Bank")]
        public int BankId { get; set; }

        public virtual Bank Bank { get; set; }

        [ForeignKey("Dealer")]
        public int? DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }

        #endregion

        #region Constructor
        public BankAccount()
        {
        }

        public BankAccount(string account, string cci, bool isDefault, int bankId)
        {
            Account = account;
            CCI = cci;
            IsDefault = isDefault;
            BankId = bankId;
            Status = Status.Active;
        }

        public BankAccount(string account, string cci, bool isDefault, int bankId, int dealerId)
        {
            Account = account;
            CCI = cci;
            IsDefault = isDefault;
            BankId = bankId;
            DealerId = dealerId;
            Status = Status.Active;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"{Bank.Abbreviation}: {BankAccountType.Name} {Currency.Abbreviation} {Account} CCI:{CCI}";
        }
        #endregion
    }
}
