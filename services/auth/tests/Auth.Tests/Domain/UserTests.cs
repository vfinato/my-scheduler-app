using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using FluentAssertions;

namespace Auth.Tests.Domain
{
    public class UserTests
    {
        [Fact]
        public void Should_Create_User_Valid_Data()
        {
            // Arrange & Act
            var email = Email.Create("test@example.com");
            var passwordHash = "HASHED_PASSWORD_123";

            var user = User.Create(email, passwordHash);

            user.Should().NotBeNull();
            user.Email.Value.Should().Be("test@example.com");
            user.PasswordHash.Should().Be(passwordHash);
            user.Id.Should().NotBe(Guid.Empty);
            user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Should_Not_Create_User_With_Null_Email()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => User.Create(null!, ""));
        }

        [Fact]
        public void Should_Not_Create_User_With_Empty_PasswordHash()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => User.Create(Email.Create("test@example.com"), ""));
        }

    }
}