using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class Charge : AuditableEntityWithKey<int>
    {
        #region Properties
        [MaxLength(20)]
        public string OperationCode { get; set; }

        [Required]
        public Money Amount { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Required]
        public ChargeStatus Status { get; set; }

        [Required]
        public ChargeType ChargeType { get; set; }

        [Required]
        public bool HasBeenRefunded { get; set; }

        [Required]
        [ForeignKey("Card")]
        public int CardId { get; set; }

        public virtual Card Card { get; set; }

        [Required]
        [ForeignKey("Cashier")]
        public int CashierId { get; set; }

        public virtual Cashier Cashier { get; set; }

        [ForeignKey("Liquidation")]
        public int? LiquidationId { get; set; }

        public virtual Liquidation Liquidation { get; set; }

        public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
        #endregion

        #region Constructor
        public Charge()
        {
        }

        public Charge(int cashierId, Card card, ChargeType chargeType, Money amount, string displayName, string description)
        {
            CashierId = cashierId;
            Card = card;
            Amount = amount;
            DisplayName = displayName;
            ChargeType = chargeType;
            Description = description;
            Status = ChargeStatus.Pending;
            var movement = new Movement(amount.Opposite(), displayName, displayName, MovementType.CON,this);
            movement.CardId = card.Id;
            Movements.Add(movement);
            if (!card.CanWithdraw(amount))
            {
                var messages = LocalizationFactory.CreateLocalResources();
                throw new InvalidOperationException(messages.GetStringResource(LocalizationKeys.Domain.exception_NoEnoughMoneyToWithdraw));
            }
            //TODO move this to a Factory Pattern instead of using the constructor
        }
        #endregion

        #region Public Methods
        public void GenerateOperationCode()
        {
            OperationCode = RandomGenerator.RandomDigits(10);
        }
        #endregion
    }
}
