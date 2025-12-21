namespace Auth.Domain.ValueObjects
{
    public sealed class Email : IEquatable<Email>
    {
        public string Value { get; }

        public override string ToString() => Value;

        private Email(string value)
        {
            Value = value;
        }

        public static Email From(string email) => Create(email);

        public static Email Create(string? email)
        {
            if(string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            email = email.Trim().ToLowerInvariant();

            if(!IsValid(email))
                throw new ArgumentException("Email format is invalid.", nameof(email));

            return new Email(email);
        }

        private static bool IsValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        #region Equality

        public bool Equals(Email? other)
            => other is not null && Value == other.Value;

        public override bool Equals(object? obj)
            => obj is Email other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
        #endregion
    }
}