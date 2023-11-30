namespace Marketplace.Domain
{
    public record Price:Money
    {
        private Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup):base(amount, currencyCode, currencyLookup)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Price can not be negative", nameof(amount));
            }
        }

        internal Price(decimal amount, string currencyCode) : base(amount, new CurrencyDetails{CurrencyCode = currencyCode})
        {
            
        }

        public static Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) => new Price(amount, currency, currencyLookup);

        public static implicit operator decimal(Price self) => self.Amount;

    }
}
