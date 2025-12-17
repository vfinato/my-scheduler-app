namespace Auth.Domain.ValueObjects
{
    public sealed class Password : IEquatable<Password>
    {
        public string Value { get; }
        private Password(string value)
        {
            Value = value;
        }

        public static Password Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password cannot be empty.");

            if (value.Length < 8)
                throw new ArgumentException("Password must have at least 8 characters.");

            if (!value.Any(char.IsLetter))
                throw new ArgumentException("Password must contain at least one letter.");

            if (!value.Any(char.IsDigit))
                throw new ArgumentException("Password must contain at least one number.");

            if (!value.Any(ch => !char.IsLetterOrDigit(ch)))
                throw new ArgumentException("Password must contain at least one special character.");
            
            if(!value.Any(char.IsUpper))
                throw new ArgumentException("Password must contain at least one uppercase letter.");
            
            if(!value.Any(char.IsLower))
                throw new ArgumentException("Password must contain at least one lowercase letter.");

            return new Password(value);
        }

        #region Equity
        public bool Equals(Password? other)
            => other is not null && Value == other.Value;

        public override bool Equals(object? obj)
         => obj is Password other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode(StringComparison.Ordinal);

        public override string ToString() => "*************";
        #endregion
    }
}