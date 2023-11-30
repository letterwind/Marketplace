namespace Marketplace.Domain
{
    public record ClassifiedAdText
    {
        private readonly string _value;
        public static ClassifiedAdText FromString(string title) => new ClassifiedAdText(title);
        internal ClassifiedAdText(string value)
        {
            _value = value;
        }

        public static implicit operator string(ClassifiedAdText self) => self._value;

    }
}
