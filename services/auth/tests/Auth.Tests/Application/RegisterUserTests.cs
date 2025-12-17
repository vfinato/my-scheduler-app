using Auth.Application.Interfaces;
using Auth.Application.RegisterUser;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Domain.ValueObjects;

using Moq;

namespace Auth.Tests.Application
{
    public class RegisterUserTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly RegisterUserUseCase _useCase;

        public RegisterUserTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();

            _useCase = new RegisterUserUseCase(
                _userRepositoryMock.Object,
                _passwordHasherMock.Object
            );
        }

        [Fact]
        public async Task Should_Create_User_When_Email_Does_Not_Exist()
        {
            var command = new RegisterUserCommand
            (
                Email: "a@b.com",
                Password: "hashedPassword123"
            );

            _passwordHasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("hashedPassword123");

            _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<Email>(), default))
                .ReturnsAsync(false);
            
            _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>(), default))
                .Returns(Task.CompletedTask);

            var result = await _useCase.ExecuteAsync(command);

            Assert.True(result.Success);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), default), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Create_User_When_Email_Exists()
        {
            var email = Email.From("a@b.com");

            var passwordHash = "hashedPassword123";

            var hasher = new Mock<IPasswordHasher>();
            hasher.Setup(h => h.Hash(passwordHash)).Returns("hashedPassword123");

            var repo = new Mock<IUserRepository>();
            repo.Setup(r => r.ExistsByEmailAsync(email, default))
                .ReturnsAsync(true);

            var useCase = new RegisterUserUseCase(repo.Object, hasher.Object);

            var command = new RegisterUserCommand
            (
                Email: email.Value,
                Password: "SomePassword!"
            );

            var result = await useCase.ExecuteAsync(command);

            Assert.False(result.Success);
            repo.Verify(r => r.AddAsync(It.IsAny<User>(), default), Times.Never);
        }
    }
}