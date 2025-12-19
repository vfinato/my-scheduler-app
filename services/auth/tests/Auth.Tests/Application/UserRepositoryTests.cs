using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Domain.ValueObjects;

using Moq;

namespace Auth.Tests.Application
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task Should_Call_AddAsync_When_Saving()
        {
            //arrange
            var repo = new Mock<IUserRepository>();

            var email = Email.From("test@example.com");
            var password = Password.FromHash("HASHED_PASSWORD_123");
            var user = User.Create(email, password);
            //act
            await repo.Object.AddAsync(user);

            //assert
            repo.Verify(r => r.AddAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Call_GetByEmailAsync()
        {   
            //Arrange & act
            var repo = new Mock<IUserRepository>();

            Email email = Email.From("example@test.com");

            await repo.Object.GetByEmailAsync(email);
            
            //Assert
            repo.Verify(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}