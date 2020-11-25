using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
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

        public BankAccount(string account, string cci, bool IsDefault, int bankId)
        {
            Account = account;
            CCI = cci;
            IsDefault = IsDefault;
            BankId = bankId;
            Status = Status.Active;
        }

        public BankAccount(string account, string cci, bool IsDefault, int bankId, int dealerId)
        {
            Account = account;
            CCI = cci;
            IsDefault = IsDefault;
            BankId = bankId;
            DealerId = dealerId;
            Status = Status.Active;
        }
        #endregion
    }
}
