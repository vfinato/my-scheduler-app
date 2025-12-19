using Auth.Domain.ValueObjects;

namespace Auth.Tests.ValueObjects
{
    public class PasswordTest
    {
        [Fact]
        public void Should_Create_Valid()
        {
            // Arrange
            var input = "Testepwd@123";

            // Act
            var password = Password.FromHash(input);

            // Assert
            Assert.Equal(input, password.Hash);
        }

        [Fact]
        public void Passwords_With_Same_Hash_Should_Be_Equal()
        {
            var hash = "hash123";

            var p1 = Password.FromHash(hash);
            var p2 = Password.FromHash(hash);

            Assert.Equal(p1, p2);
            Assert.True(p1.Equals(p2));
            Assert.Equal(p1.GetHashCode(), p2.GetHashCode());
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void Should_Not_Create_Invalid(string? invalid)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Password.FromHash(invalid));
        }
    }
}