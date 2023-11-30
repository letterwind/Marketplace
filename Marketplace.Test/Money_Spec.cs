using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Domain;

namespace Marketplace.Test
{
    public class Money_Spec
    {
        private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

        [Fact]
        public void Two_of_same_amount_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);
            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_of_same_amount_but_different_currencies()
        {
            var firstAmount = Money.FromDecimal(1, "EUR", CurrencyLookup);
            var secondAmount = Money.FromDecimal(1, "USD", CurrencyLookup);

            Assert.NotEqual(firstAmount, secondAmount);
        }

        [Fact]
        public void FromString_and_FromeDecimal_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(1, "EUR", CurrencyLookup);
            var secondAmount = Money.FromString("1.00", "EUR", CurrencyLookup);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Unused_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(1, "DEM", CurrencyLookup));
        }

        [Fact]
        public void Unknown_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(1, "What?", CurrencyLookup));
        }

        [Fact]
        public void Throws_when_too_many_decimal_places()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Money.FromDecimal(1.222m, "EUR", CurrencyLookup));
        }

        [Fact]
        public void Throws_on_adding_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "USD", CurrencyLookup);
            Assert.Throws<CurrencyMismatchException>(() => firstAmount + secondAmount);
        }

        [Fact]
        public void Throws_on_subtract_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "USD", CurrencyLookup);
            Assert.Throws<CurrencyMismatchException>(() => firstAmount - secondAmount);
        }

        [Fact]
        public void Sum_of_money_gives_full_amount()
        {
            var coin1 = Money.FromDecimal(1, "EUR", CurrencyLookup);
            var coin2 = Money.FromDecimal(2, "EUR", CurrencyLookup);
            var coin3 = Money.FromDecimal(2, "EUR", CurrencyLookup);
            var banknote = Money.FromDecimal(5, "EUR", CurrencyLookup);

            Assert.Equal(banknote, coin1 + coin2 + coin3);
        }
    }
}
