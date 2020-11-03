using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.CrossCutting.Localization;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class Card : AuditableEntityWithKey<int>
    {
        #region Properties
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        public Money Balance { get; set; }

        [MaxLength(100)]
        public string TPCode { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime? ArchiveAt { get; set; }

        [Required]
        [ForeignKey("CardType")]
        public int CardTypeId { get; set; }

        public virtual CardType CardType { get; set; }

        [Required]
        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual ICollection<Charge> Charges { get; } = new List<Charge>();

        public virtual ICollection<Movement> Movements { get; } = new List<Movement>();

        public virtual ICollection<CardBatch> CardBatches { get; } = new List<CardBatch>();

        public virtual ICollection<Transfer> OriginTransfers { get; } = new List<Transfer>();

        public virtual ICollection<Transfer> DestinyTransfers { get; } = new List<Transfer>();
        #endregion

        #region NotMapped Properties

        [NotMapped]
        public IEnumerable<CardBatch> AvailableCardBatches {
            get => CardBatches.Where(cb => cb.Balance.Value > 0 && cb.Batch.ExpiresAtExtent > DateTime.Now).OrderBy(cb => cb.Batch.ExpiresAtExtent);
        }

        [NotMapped]
        public IEnumerable<CardBatch> CardBatchesWithBalance
        {
            get => CardBatches.Where(cb => cb.Balance.Value > 0).OrderByDescending(cb => cb.Batch.ExpiresAtExtent);
        }

        [NotMapped]
        public Money CalculatedBalance
        {
            get
            {
                return Balance;
                var calculatedBalance = new Money(Balance.Currency, 0);
                var cardBatches = AvailableCardBatches;
                foreach (var cardBatch in cardBatches)
                {
                    calculatedBalance = calculatedBalance.Add(cardBatch.Balance);
                }
                return calculatedBalance;
            }
        }
        #endregion

        #region Constructor
        public Card()
        {
        }

        public Card(Contact contact, Currency currency, int cardTypeId, string tpCode = null)
        {
            Contact = contact;
            Balance = new Money(currency, 0);
            Status = Status.Active;
            CardTypeId = cardTypeId;
            Code = RandomGenerator.RandomDigits(20);
            TPCode = tpCode;
            AddCreationMovement();
        }
        #endregion

        #region Private Methods
        private List<BatchMovement> GetBatchMovementsList(List<BatchMovement> batchMovements)
        {
            return batchMovements.Select(bm => new BatchMovement()
            {
                Amount = bm.Amount.Abs(),
                BatchId = bm.BatchId,
            }).ToList();
        }

        private void AddMovement(Money amount, MovementType movementType, string description, string displayName,
                                List<BatchMovement> batchMovements = null,Transfer transfer = null)
        {
            Movements.Add(new Movement(amount, description, displayName, movementType, batchMovements, transfer));
            Balance = Balance.Add(amount);
        }

        private void AddCreationMovement()
        {
            var messages = LocalizationFactory.CreateLocalResources();
            var money = new Money(Balance.Currency, 0);
            AddMovement(money, MovementType.CTA,
                        messages.GetStringResource(LocalizationKeys.Domain.messages_creationCardMessageDescription),
                        messages.GetStringResource(LocalizationKeys.Domain.messages_creationCardMessageDisplayName));
        }
        #endregion

        #region Public Methods
        public bool CanWithdraw(Money amount)
        {
            return amount.IsLessOrEqualThan(CalculatedBalance);
        }

        public void RechargeMoney(Batch batch, string description, string displayName)
        {
            batch.CardBatches.Add(
                new CardBatch(){
                    Balance = batch.Amount,
                    Card = this
                }
            );
            var batchMovements = new List<BatchMovement>()
            {
                new BatchMovement()
                {
                    Amount = batch.Amount,
                    Batch = batch
                }
            };
            AddMovement(batch.Amount, MovementType.REC, description, displayName, batchMovements);
        }

        public List<BatchMovement> WithdrawMoney(Money amountNeeded, MovementType movementType, string description, string displayName,
                                                Movement movement = null,Transfer transfer = null)
        {
            var batchMovements = new List<BatchMovement>();
            var amountTaken = new Money(amountNeeded.Currency, 0.0);
            var cardBatches = AvailableCardBatches;

            foreach (var cardBatch in cardBatches)
            {
                if (amountTaken.IsLessThan(amountNeeded))
                {
                    var amountRemaining = amountNeeded.Substract(amountTaken);
                    var amountToAdd = cardBatch.Balance.Min(amountRemaining);
                    batchMovements.Add(new BatchMovement()
                    {
                        BatchId = cardBatch.BatchId,
                        Amount = amountToAdd.Opposite(),
                        Movement = movement
                    });
                    cardBatch.Balance = cardBatch.Balance.Substract(amountToAdd);
                    amountTaken = amountTaken.Add(amountToAdd);
                }
            }
            if (movement == null)
            {
                AddMovement(amountNeeded.Opposite(), movementType, description, displayName, batchMovements, transfer);
            }
            return batchMovements;
        }

        public void DepositMoneyFromTransfer(Money money, List<BatchMovement> batchMovements, string description, 
                                            string displayName, Transfer transfer)
        {
            foreach (var batchMovement in batchMovements)
            {
                var existingBatch = CardBatches.FirstOrDefault(cb => cb.BatchId == batchMovement.BatchId);
                if (existingBatch == null)
                {
                    CardBatches.Add(new CardBatch
                    {
                        BatchId = batchMovement.BatchId,
                        Balance = batchMovement.Amount.Abs()
                    });
                }
                else
                {
                    existingBatch.Balance = existingBatch.Balance.Add(batchMovement.Amount.Abs());
                }
            }

            AddMovement(money, MovementType.ITR, description, displayName, GetBatchMovementsList(batchMovements), transfer);
        }
        #endregion
    }
}
