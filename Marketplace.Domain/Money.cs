namespace Marketplace.Domain
{
    public record class Money
    {
        protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentNullException(nameof(currencyCode), "Currency code must be specified");
            }

            var currency = currencyLookup.FindCurrency(currencyCode);

            if (currency.InUse == false)
            {
                throw new ArgumentException(nameof(currencyCode), $"Currency {currencyCode} is not valid");
            }

            if (decimal.Round(amount, currency.DecimalPlaces) != amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount can not have more than two decimals");
            }

            Amount = amount;
            Currency = currency;
        }

        protected Money(decimal amount, CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) => new (amount, currency, currencyLookup);
        public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) => new (decimal.Parse(amount), currency, currencyLookup);

        public decimal Amount { get; set; }
        public CurrencyDetails Currency { get; }

        public Money Add(Money summand)
        {
            if (summand.Currency != Currency)
            {
                throw new CurrencyMismatchException("can not sum amount with different currencies");
            }
            return new(Amount + summand.Amount, Currency);
        }

        public Money Subtract(Money subtract)
        {
            if (subtract.Currency!= Currency)
            {
                throw new CurrencyMismatchException("can not subtract amount with different currencies");
            }

            return new(Amount - subtract.Amount, Currency);
        } 

        public static Money operator +(Money a, Money b)=>a.Add(b);
        public static Money operator -(Money a, Money b)=>a.Subtract(b);

        public override string ToString()
        {
            return $"{Currency.CurrencyCode} {Amount}";
        }
    }

    public class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException(string message): base(message)
        {
            
        }
    }
    //public class Money:IEquatable<Money>
    //{
    //    public decimal Amount { get; }

    //    public Money(decimal amount) => Amount = amount;

    //    public bool Equals(Money? other)
    //    {
    //        if (ReferenceEquals(null, other)) return false;
    //        if (ReferenceEquals(this, other)) return true;
    //        return Amount == other.Amount;
    //    }

    //    public override bool Equals(object? obj)
    //    {
    //        if (ReferenceEquals(null, obj)) return false;
    //        if (ReferenceEquals(this, obj)) return true;
    //        if (obj.GetType() != this.GetType()) return false;
    //        return Equals((Money) obj);
    //    }

    //    public override int GetHashCode()=> Amount.GetHashCode();

    //    public static bool operator ==(Money? left, Money? right) => Equals(left, right);
    //    public static bool operator !=(Money? left, Money? right) => !Equals(left, right);
    //}
}
