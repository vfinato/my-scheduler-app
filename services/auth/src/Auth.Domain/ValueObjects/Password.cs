namespace Auth.Domain.ValueObjects
{
    public sealed class Password : IEquatable<Password>
    {
        public string Hash { get; }
        private Password(string hash)
        {
            Hash = hash;
        }

        public static Password FromHash(string? hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Password cannot be empty.");

            return new Password(hash);
        }

        #region Equity
        public bool Equals(Password? other)
            => other is not null && Hash == other.Hash;

        public override bool Equals(object? obj)
         => obj is Password other && Equals(other);

        public override int GetHashCode()
            => Hash.GetHashCode(StringComparison.Ordinal);

        public override string ToString() => "*************";
        #endregion
    }
}