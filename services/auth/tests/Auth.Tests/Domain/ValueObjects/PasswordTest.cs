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
            var password = Password.Create(input);

            // Assert
            Assert.Equal(input, password.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        [InlineData("short")]
        [InlineData("nouppercase1@")]
        [InlineData("NOLOWERCASE1@")]
        [InlineData("NoNumber@")]
        [InlineData("NoSpecialChar1")]
        [InlineData("123456789")]
        [InlineData("abcdefgh")]
        public void Should_Not_Create_Invalid(string? invalid)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Password.Create(invalid));
        }
    }
}