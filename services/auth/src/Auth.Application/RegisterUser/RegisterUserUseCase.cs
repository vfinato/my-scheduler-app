using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Domain.ValueObjects;

namespace Auth.Application.RegisterUser
{
    public sealed class RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        public async Task<RegisterUserResult> ExecuteAsync(RegisterUserCommand command, CancellationToken ct = default)
        {
            Email email;

            try
            {
                email = Email.From(command.Email);
            }
        
            catch (ArgumentException ex)
            {
                return RegisterUserResult.Fail($"Invalid email: {ex.Message}");
            }

            if(await userRepository.ExistsByEmailAsync(email, ct))
                return RegisterUserResult.Fail("Email already in use.");
            
            var hash = passwordHasher.Hash(command.Password);

            var password = Password.FromHash(hash);

            var user = User.Create(email, password);

            await userRepository.AddAsync(user, ct);

            return RegisterUserResult.Ok(user.Id);
        }
    }
}