using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.DealerAgg
{
    public class Dealer : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]
        public string Ruc { get; set; }

        [Required]
        public DealerType Type { get; set; }

        [MaxLength(100)]
        public string ContactName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Zone { get; set; }

        [Required]
        public int MaxCashiers { get; set; }

        [MaxLength(100)]
        public string TPCode { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public virtual ICollection<Cashier> Cashiers { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        #endregion

        #region Constructor
        public Dealer()
        {
        }

        public Dealer(string tpCode, string phone, string address,
                      string contactName, string name, string ruc,
                      int maxCashiers, DealerType type)
        {
            TPCode = tpCode;
            Phone = phone;
            Address = address;
            ContactName = contactName;
            Name = name;
            Ruc = ruc;
            MaxCashiers = maxCashiers;
            Type = type;
            Status = Status.Active;
        }
        #endregion

        #region Public Methods
        public BankAccount GetBankAccount(int bankId, int currencyId)
        {
            var defaultBankAccount = BankAccounts.FirstOrDefault(ba => ba.BankId == bankId && ba.CurrencyId == currencyId && ba.IsDefault && ba.Status == Status.Active);

            if (defaultBankAccount == null)
            {
                defaultBankAccount = BankAccounts.FirstOrDefault(ba => ba.BankId == bankId && ba.CurrencyId == currencyId && ba.Status == Status.Active);
                if (defaultBankAccount == null)
                {
                    defaultBankAccount = BankAccounts.FirstOrDefault(ba => ba.CurrencyId == currencyId && ba.Status == Status.Active);
                }
            }
            return defaultBankAccount;
        }
        #endregion
    }
}
