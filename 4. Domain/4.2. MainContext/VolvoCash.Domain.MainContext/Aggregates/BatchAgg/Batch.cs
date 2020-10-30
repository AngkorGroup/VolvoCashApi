using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.BatchAgg
{
    public class Batch : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        public Money Amount { get; set; }

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

        [Required]
        public TPContractType TPContractType { get; set; }

        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        public string BusinessCode { get; set; }

        public string BusinessDescription { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("CardType")]
        public int? CardTypeId { get; set; }

        public virtual CardType CardType { get; set; }

        public virtual ICollection<BatchMovement> BatchMovements { get; } = new List<BatchMovement>();

        public virtual ICollection<CardBatch> CardBatches { get; } = new List<CardBatch>();

        [MaxLength(4000)]
        public string LineContent { get; set; }
        #endregion

        #region Constructor
        public Batch()
        {
        }

        public Batch(string tpContractBatchNumber,Money amount, DateTime expire, string tpChasis, DateTime? tpContractDate, string tpInvoiceCode,
                    DateTime? tpInvoiceDate, TPContractType tpContractType, string tpContractNumber, string tpContractReason,
                    string dealerCode, string dealerName, string businessCode, string businessDescription,
                    int cardTypeId,string lineContent)
        {
            TPContractBatchNumber = tpContractBatchNumber;
            Amount = amount;
            ExpiresAt = expire.Date.AddDays(1);
            ExpiresAtExtent = ExpiresAt;
            TPChasis = tpChasis;
            TPContractDate = tpContractDate;
            TPInvoiceCode = tpInvoiceCode;
            TPInvoiceDate = tpInvoiceDate;
            TPContractType = tpContractType;
            TPContractNumber = tpContractNumber;
            TPReason = tpContractReason;
            DealerCode = dealerCode;
            DealerName = dealerName;
            BusinessCode = businessCode;
            BusinessDescription = businessDescription;
            CardTypeId = cardTypeId;
            LineContent = lineContent;
        }
        #endregion
    }
}
