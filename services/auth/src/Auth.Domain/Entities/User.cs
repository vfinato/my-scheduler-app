using Auth.Domain.ValueObjects;

namespace Auth.Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        private User(){}

        private User(Guid id, Email email, Password password, DateTime createdAt, bool isActive)
        {
            Id = id;
            Email = email;
            Password = password;
            CreatedAt = createdAt;
            IsActive = isActive;
        }

        

        public static User Create(Email email, Password password)
        {
            
            if(email == null)
                throw new ArgumentException("Password cannot be empty.", nameof(email));

            if(password == null)
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            return new User(
                id: Guid.NewGuid(),
                email: email,
                password: password,
                createdAt: DateTime.UtcNow,
                isActive: true
            );
        }

        public void ChangePassword(Password newPassword)
        {
            ArgumentNullException.ThrowIfNull(newPassword);
            Password = newPassword;
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