﻿namespace Marketplace.Domain
{
    public class ClassifiedAd : Entity
    {
        public ClassifiedAd(ClassifiedAdId Id, UserId OwnerId) => Apply(new Events.ClassifiedAdCreated
        {
            Id = Id,
            OwnerId = OwnerId
        });

        public void SetTitle(ClassifiedAdTitle title) => Apply(new Events.ClassifiedAdTitleChanged
        {
            Id = Id,
            Title = title
        });
        public void UpdateText(ClassifiedAdText text) => Apply(new Events.ClassifiedAdTextUpdated
        {
            Id = Id,
            AdText = text
        });
        public void UpdatePrice(Price price) => Apply(new Events.ClassifiedAdPriceUpdated
        {
            Id = Id,
            Price = price.Amount
        });

        public void RequestToPublish() => Apply(new Events.ClassifiedAdSentForReview { Id = Id });

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ClassifiedAdCreated e:
                    this.Id = new ClassifiedAdId(e.Id);
                    this.OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;

                    break;
                case Events.ClassifiedAdTitleChanged e:
                    Title = new ClassifiedAdTitle(e.Title);
                    break;
                case Events.ClassifiedAdTextUpdated e:
                    Text = new ClassifiedAdText(e.AdText);
                    break;
                case Events.ClassifiedAdPriceUpdated e:
                    Price = new Price(e.Price, e.CurrencyCode);
                    break;
                case Events.ClassifiedAdSentForReview e:
                    State = ClassifiedAdState.PendingReview;
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            var valid = Id != null && OwnerId != null &&
                        (State switch
                        {
                            ClassifiedAdState.PendingReview =>
                                Title != null && Text != null && Price?.Amount > 0,
                            ClassifiedAdState.Active =>
                                Title != null && Text != null && Price?.Amount > 0 && ApprovedBy != null,
                            _ => true
                        });
            if (!valid)
            {
                throw new InvalidEntityStateException(this, $"Post-checks faild in state {State}");
            }
        }

        public ClassifiedAdTitle? Title { get; private set; }
        public ClassifiedAdText? Text { get; private set; }
        public Price? Price { get; private set; }
        public ClassifiedAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }
        public ClassifiedAdId Id { get; private set; }
        public UserId OwnerId { get; private set; }

        public void Deconstruct(out ClassifiedAdId id, out UserId ownerId)
        {
            id = this.Id;
            ownerId = this.OwnerId;
        }
    }
}
