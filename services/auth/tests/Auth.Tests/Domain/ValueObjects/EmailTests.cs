using Auth.Domain.ValueObjects;

namespace Auth.Tests.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Should_Create_Email_Valid()
        {
            // Arrange
            var input = "TesteUser@Email.com";

            // Act
            var email = Email.Create(input);

            // Assert
            Assert.Equal("testeuser@email.com", email.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        [InlineData("invalid-email")]
        [InlineData("user@")]
        [InlineData("@domain.com")]
        public void Should_Throw_When_Invalid(string? invalid)
        {
            Assert.Throws<ArgumentException>(() => Email.Create(invalid));
        }

    }
}