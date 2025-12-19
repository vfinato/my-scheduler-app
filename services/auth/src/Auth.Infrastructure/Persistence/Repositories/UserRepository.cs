using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Domain.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories
{
    internal sealed class UserRepository(AuthDbContext _context) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _context.Users.AddAsync(user, ct);
        }

        public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default)
        {
            return await _context.Users.AnyAsync(x => x.Email.Value == email.Value, ct);
        }

        public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Value == email.Value, ct);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
        }
    }
}