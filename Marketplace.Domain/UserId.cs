namespace Marketplace.Domain
{
    public record UserId(Guid value)
    {
        public static implicit operator Guid(UserId self) => self.value;

    }
}
