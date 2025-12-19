using Auth.Application.Interfaces;
using Auth.Application.RegisterUser;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Domain.ValueObjects;

using Moq;

namespace Auth.Tests.UseCases
{
    public class RegisterUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly RegisterUserUseCase _useCase;

        public RegisterUserUseCaseTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();

            _useCase = new RegisterUserUseCase(
                _userRepositoryMock.Object,
                _passwordHasherMock.Object
            );
        }

        [Fact]
        public async Task Should_Register_When_Data_IsValid()
        {
            var command = new RegisterUserCommand
            (
                Email: "celorde@AgroDev.com",
                Password: "StrongPassword123!"
            );

            _userRepositoryMock
                .Setup(repo => repo.ExistsByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            
            _passwordHasherMock
                .Setup(hasher => hasher.Hash(command.Password))
                .Returns("hashed_password");
            
            var result = await _useCase.ExecuteAsync(command);


            Assert.True(result.Success);
            Assert.NotNull(result.UserId);

            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Register_When_Email_Exists()
        {
            var command = new RegisterUserCommand
            (
                Email: "a@b.com",
                Password: "hashedPassword123"
            );

            _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await _useCase.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.Equal("Email already in use.", result.ErrorMessage);

            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Should_Fail_When_Email_Is_Invalid()
        {
            var command = new RegisterUserCommand(
                Email: "invalido",
                Password: "SomePassword123!"
            );

            var result = await _useCase.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.NotNull(result.ErrorMessage);

            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}