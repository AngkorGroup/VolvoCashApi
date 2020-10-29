using System;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class Money : ValueObject<Money>
    {
        #region Properties
        public Currency Currency { get; set; }

        public double Value { get; set; }
        #endregion

        #region Constructor
        public Money()
        {
        }

        public Money(Currency currency, double value)
        {
            Currency = currency;
            Value = value;
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
                //TODO aplicar tipo de cambio
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
                //TODO aplicar tipo de cambio
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
        #endregion
    }
}
