using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BankAgg
{
    public class Bank : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string TPCode { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<BankDocumentType> BankDocumentTypes { get; } = new List<BankDocumentType>();
        
        public virtual ICollection<BankBankAccountType> BankBankAccountTypes { get; } = new List<BankBankAccountType>();
        
        public virtual ICollection<BankCurrency> BankCurrencies { get; } = new List<BankCurrency>();

        public virtual ICollection<BankAccount> BankAccounts { get; } = new List<BankAccount>();
        #endregion

        #region Constructor
        public Bank()
        {
        }

        public Bank(string name, string abbreviation, string tpCode)
        {
            Name = name;
            Abbreviation = abbreviation;
            TPCode = tpCode;
            Status = Status.Active;
        }
        #endregion
    }
}
