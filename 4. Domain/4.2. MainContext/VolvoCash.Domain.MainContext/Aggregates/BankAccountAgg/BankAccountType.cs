using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg
{
    public class BankAccountType : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<BankBankAccountType> BankBankAccountTypes { get; } = new List<BankBankAccountType>();
        #endregion

        #region Constructor
        public BankAccountType()
        {
        }

        public BankAccountType(string name)
        {
            Name = name;
            Status = Status.Active;
        }
        #endregion
    }
}
