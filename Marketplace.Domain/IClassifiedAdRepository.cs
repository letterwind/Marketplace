
namespace Marketplace.Domain
{
    public interface IClassifiedAdRepository
    {
        Task<ClassifiedAd> Load(ClassifiedAdId id);
        Task Save(ClassifiedAd entity);
        Task<bool> Exists(ClassifiedAdId id);
    }
}
