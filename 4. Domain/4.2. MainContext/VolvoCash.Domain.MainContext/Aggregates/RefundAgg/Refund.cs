using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.RefundAgg
{
    public class Refund : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public Money Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public RefundStatus RefundStatus { get; set; }

        public int LiquidationsCount { get; set; }

        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        [Required]
        public string CompanyBankAccount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Voucher { get; set; }

        public virtual ICollection<Liquidation> Liquidations { get; set; } = new List<Liquidation>();

        #endregion

        #region Constructor
        public Refund(DateTime date, Money amount, BankAccount bankAccount, List<Liquidation> liquidations)
        {
            Date = date;
            Amount = amount;
            BankAccount = bankAccount;
            BankAccountId = bankAccount.Id;
            CompanyBankAccount = bankAccount.ToString();
            RefundStatus = RefundStatus.Scheduled;
            Liquidations = liquidations;
            LiquidationsCount = liquidations.Count;
        }

        public Refund()
        {
        }
        #endregion

        #region Public Methods
        public void PayRefund(string voucher, DateTime paymentDate)
        {
            Voucher = voucher;
            PaymentDate = paymentDate;
            RefundStatus = RefundStatus.Paid;

            foreach (var liquidation in Liquidations)
            {
                liquidation.Voucher = voucher;
                liquidation.PaymentDate = paymentDate;
                liquidation.LiquidationStatus = LiquidationStatus.Paid;
                foreach (var charge in liquidation.Charges)
                {
                    charge.HasBeenRefunded = true;
                }
            }
        }
        #endregion
    }
}
