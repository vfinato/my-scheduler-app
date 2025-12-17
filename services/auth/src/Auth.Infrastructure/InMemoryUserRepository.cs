using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Domain.ValueObjects;

namespace Auth.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        public Task AddAsync(User user, CancellationToken ct = default)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = _users.FirstOrDefault(u => u.Email.Value == email.ToLowerInvariant());
            return Task.FromResult(user);
        }

        public Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}