using Marketplace.Domain;

namespace Marketplace.Test
{
    public class ClassifiedAd_Publish_Spec
    {
        private readonly ClassifiedAd _classifiedAd;

        public ClassifiedAd_Publish_Spec()
        {
            _classifiedAd = new ClassifiedAd(new ClassifiedAdId(Guid.NewGuid()), new UserId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_publish_a_valid_ad()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("ad Text"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(5, "USD", new FakeCurrencyLookup()));
            _classifiedAd.RequestToPublish();
            Assert.Equal(ClassifiedAdState.PendingReview, _classifiedAd.State);
        }

        [Fact]
        public void Cannot_publish_without_title()
        {
            
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("ad Text"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(5, "USD", new FakeCurrencyLookup()));
            
            Assert.Throws<InvalidEntityStateException>(()=> _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_text()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(5, "USD", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_price()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("ad Text"));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_with_zero_price()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("ad Text"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(0, "USD", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }
    }
}
