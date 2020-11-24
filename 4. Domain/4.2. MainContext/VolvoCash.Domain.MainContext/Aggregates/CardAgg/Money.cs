using System;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class Money : ValueObject<Money>
    {
        #region Properties
        public double Value { get; set; }

        [ForeignKey("Currency")]
        public int? CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
        #endregion

        #region Constructor
        public Money()
        {
        }

        public Money(Currency currency, double value)
        {
            Currency = currency;
            CurrencyId = currency?.Id;
            Value = value;
        }

        public Money(Money amount)
        {
            Currency = amount.Currency;
            CurrencyId = amount?.Currency?.Id;
            Value = amount.Value;
        }
        #endregion

        #region Public Methods
        public Money Add(Money other)
        {
            var amount = Value;
            if (Currency == other.Currency)
            {
                amount += other.Value;
            }
            else
            {
                var exchangeRate = 1;
                amount += other.Value * exchangeRate;
                //TODO: aplicar tipo de cambio
            }
            return new Money(Currency, amount);
        }

        public Money Substract(Money other)
        {
            var amount = Value;
            if (Currency == other.Currency)
            {
                amount -= other.Value;
            }
            else
            {
                var exchangeRate = 1;
                amount -= other.Value * exchangeRate;
                //TODO: aplicar tipo de cambio
            }
            return new Money(Currency, amount);
        }

        public bool IsLessThan(Money other)
        {
            if (Currency == other.Currency)
            {
                return Value < other.Value;
            }
            else
            {
                //TODO aplicar tipo de cambio
                var exchangeRate = 1;
                return Value < other.Value * exchangeRate;
            }
        }

        public Money Min(Money other)
        {
            if (IsLessThan(other))
            {
                return this;
            }
            return other;
        }

        public Money Opposite()
        {
            return new Money(Currency, Value * -1);
        }

        public Money Abs()
        {
            return new Money(Currency, Math.Abs(Value));
        }

        public bool IsLessOrEqualThan(Money other)
        {
            if (Currency == other.Currency)
            {
                return Value <= other.Value;
            }
            else
            {
                //TODO aplicar tipo de cambio
                var exchangeRate = 1;
                return Value <= other.Value * exchangeRate;
            }
        }

        public string GetLabel()
        {
            return Currency.ToString() + " " + string.Format("{0:#,0.00}", Value);
        }
        #endregion
    }
}
