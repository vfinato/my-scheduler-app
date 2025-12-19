using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;

namespace Auth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);
        Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default);
    }
}