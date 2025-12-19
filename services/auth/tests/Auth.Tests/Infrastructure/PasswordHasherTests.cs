using Auth.Application.Interfaces;
using Auth.Infrastructure.Services;

namespace Auth.Tests.Infrastructure
{
    public class PasswordHasherTests
    {
        private readonly IPasswordHasher _passwordHasher;

        public PasswordHasherTests()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Fact]
        public void Should_return_a_hash_for_given_pass()
        {
            var plainPassword = "Strong@Password123";

            var hash = _passwordHasher.Hash(plainPassword);

            Assert.NotNull(hash);
            Assert.NotEqual(plainPassword, hash);
        }
    }
}