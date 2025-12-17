using Auth.Domain.ValueObjects;

namespace Auth.Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        private User(){}

        private User(Guid id, Email email, string passwordHash, DateTime createdAt, bool isActive)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = createdAt;
            IsActive = isActive;
        }

        

        public static User Create(Email email, string passwordHash)
        {
            if(email == null)
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null.");
            }
            
            if(string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
            }

            return new User(
                id: Guid.NewGuid(),
                email: email,
                passwordHash: passwordHash,
                createdAt: DateTime.UtcNow,
                isActive: true
            );
        }

        public void ChangePassword(string newPasswordHash)
        {
            if(string.IsNullOrWhiteSpace(newPasswordHash))
            {
                throw new ArgumentException("New password hash cannot be empty.", nameof(newPasswordHash));
            }
            PasswordHash = newPasswordHash;
        }

        public void Deactivate()
        {
            if(!IsActive)
                return;

            IsActive = false;
        }

        public void Activate()
        {
            if(IsActive)
                return;

            IsActive = true;
        }
    }
}