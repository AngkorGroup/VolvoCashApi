using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
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

        public DateTime Date { get; set; }

        [ForeignKey("Dealer")]
        public int DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }

        [ForeignKey("BankAccount")]
        public int? BankAccountId { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public string CompanyBankAccount { get; set; }

        public string DealerBankAccount { get; set; }

        public LiquidationStatus LiquidationStatus { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int ChargesCount { get; set; }

        public string Voucher { get; set; }

        public virtual ICollection<Charge> Charges { get; set; } = new List<Charge>();

        #endregion

        #region Constructor
        public Liquidation()
        {
        }

        public Liquidation(DateTime date, Money amount, int dealerId)
        {
            Date = date;
            Amount = amount;
            DealerId = dealerId;
            ChargesCount = 0;
            LiquidationStatus = LiquidationStatus.Generated;
        }
        #endregion

        #region Public Methods
        public void AddCharge(Charge charge)
        {
            Charges.Add(charge);
            ChargesCount++;
        }

        public void AddAmount(Money amount)
        {
            Amount = Amount.Add(amount);
        }

        public void ScheduleLiquidation(BankAccount bankAccount)
        {
            BankAccount = bankAccount;
            LiquidationStatus = LiquidationStatus.Scheduled;
            CompanyBankAccount = bankAccount.ToString();
        }

        public void SetDealerBankAccount(BankAccount dealerBankAccount)
        {
            DealerBankAccount = dealerBankAccount.ToString();
        }

        public void PayLiquidation(string voucher, DateTime paymentDate)
        {
            Voucher = voucher;
            PaymentDate = paymentDate;
            LiquidationStatus = LiquidationStatus.Paid;
            foreach (var charge in Charges)
            {
                charge.HasBeenRefunded = true;
            }
        }

        public void CancelLiquidation()
        {
            foreach (var charge in Charges)
            {
                charge.LiquidationId = null;
            }
            LiquidationStatus = LiquidationStatus.Canceled;
        }
        #endregion
    }
}
