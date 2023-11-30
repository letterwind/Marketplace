namespace Marketplace.Domain
{
    public record ClassifiedAdId(Guid value)
    {
        public static implicit operator Guid(ClassifiedAdId self) => self.value;
    }
}
