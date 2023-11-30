namespace Marketplace.Domain
{
    public record ClassifiedAdTitle
    {
        private readonly string _value;

        public static ClassifiedAdTitle FromString(string title)
        {
            CheckValidity(title);
            return new ClassifiedAdTitle(title);
        } 
        internal ClassifiedAdTitle(string value)
        {
            _value = value;
        }

        private static void CheckValidity(string value)
        {
            if (value.Length > 100)
            {
                throw new ArgumentOutOfRangeException("Title can not be longer than 100 characters", nameof(value));
            }
        }

        public static implicit operator string(ClassifiedAdTitle self) => self._value;

    }
}
