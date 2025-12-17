using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Domain.ValueObjects;

namespace Auth.Application.RegisterUser
{
    public sealed class RegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

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

            if(await _userRepository.ExistsByEmailAsync(email, ct))
                return RegisterUserResult.Fail("Email already in use.");
            
            var password = _passwordHasher.Hash(command.Password);

            var user = User.Create(email, password);

            await _userRepository.AddAsync(user, ct);

            return RegisterUserResult.Ok(user.Id);
        }
    }
}