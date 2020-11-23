using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BatchAgg
{
    public class Batch : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public Money Amount { get; set; }

        public Money Balance { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public DateTime ExpiresAtExtent { get; set; }

        public DateTime? TPContractDate { get; set; }

        [MaxLength(20)]
        public string TPChasis { get; set; }

        public DateTime? TPInvoiceDate { get; set; }

        public string TPInvoiceCode { get; set; }

        public string TPContractNumber { get; set; }

        public string TPContractBatchNumber { get; set; }

        public string TPReason { get; set; }

        [ForeignKey("RechargeType")]
        public int ? RechargeTypeId { get; set; }

        public virtual RechargeType RechargeType { get; set; }

        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        [ForeignKey("BusinessArea")]
        public int? BusinessAreaId { get; set; }

        public virtual BusinessArea BusinessArea { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("CardType")]
        public int? CardTypeId { get; set; }

        public virtual CardType CardType { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [ForeignKey("Card")]
        public int CardId { get; set; }

        public virtual Card Card { get; set; }

        public virtual ICollection<BatchMovement> BatchMovements { get; } = new List<BatchMovement>();

        public virtual ICollection<CardBatch> CardBatches { get; } = new List<CardBatch>();

        [MaxLength(4000)]
        public string LineContent { get; set; }
        #endregion

        #region Constructor
        public Batch()
        {
        }

        public Batch(Contact contact, Card card, string tpContractBatchNumber, Money amount, DateTime expire, string tpChasis,
                    DateTime? tpContractDate, string tpInvoiceCode,DateTime? tpInvoiceDate, int rechargeTypeId, 
                    string tpContractNumber, string tpContractReason, string dealerCode, string dealerName, 
                    int businessAreaId,  int cardTypeId, string lineContent)
        {
            Contact = contact;
            Card = card;
            TPContractBatchNumber = tpContractBatchNumber;
            Amount = new Money(amount);
            Balance = new Money(amount);
            ExpiresAt = expire.Date.AddDays(1);
            ExpiresAtExtent = ExpiresAt;
            TPChasis = tpChasis;
            TPContractDate = tpContractDate;
            TPInvoiceCode = tpInvoiceCode;
            TPInvoiceDate = tpInvoiceDate;
            RechargeTypeId = rechargeTypeId;
            TPContractNumber = tpContractNumber;
            TPReason = tpContractReason;
            DealerCode = dealerCode;
            DealerName = dealerName;
            BusinessAreaId = businessAreaId;
            CardTypeId = cardTypeId;
            LineContent = lineContent;
        }
        #endregion
    }
}
