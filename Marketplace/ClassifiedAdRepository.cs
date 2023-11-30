using Marketplace.Domain;

namespace Marketplace
{
    public class ClassifiedAdRepository : IClassifiedAdRepository
    {
        public Task<bool> Exists(ClassifiedAdId id)
        {
            throw new NotImplementedException();
        }

        public Task<ClassifiedAd> Load(ClassifiedAdId id)
        {
            throw new NotImplementedException();
        }

        public Task Save(ClassifiedAd entity)
        {
            throw new NotImplementedException();
        }
    }
}
