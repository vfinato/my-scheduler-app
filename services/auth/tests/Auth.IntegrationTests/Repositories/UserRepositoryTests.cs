using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Auth.IntegrationTests;

public sealed class UserRepositoryTests (DbFixture fixture) : IClassFixture<DbFixture>
{
    private DbContextOptions<AuthDbContext> CreateOptions(string dbName)
    {
        // Usamos a ConnectionString da fixture e apenas trocamos o Database
        var builder = new NpgsqlConnectionStringBuilder(fixture.BaseConnectionString)
        {
            Database = dbName
        };

        return new DbContextOptionsBuilder<AuthDbContext>()
            .UseNpgsql(builder.ConnectionString)
            .Options;
    }

    [Fact]
    public async Task Should_Save_And_Retrieve_User_By_Email()
    {
        // Arrange
        var dbName = $"auth_test_{Guid.NewGuid()}";
        var options = CreateOptions(dbName);

        await using (var setupContext = new AuthDbContext(options))
        {
            await setupContext.Database.EnsureDeletedAsync();
            await setupContext.Database.EnsureCreatedAsync();
        }
        try
        {
            var email = Email.Create("john@doe.com");
            var password = Password.FromHash("hashed-password");

            var user = User.Create(email, password);

            // Act
            await using (var context = new AuthDbContext(options))
            {
                var repository = new UserRepository(context);

                await repository.AddAsync(user);
                await context.SaveChangesAsync();
            }

            // Assert
            await using (var assertContext = new AuthDbContext(options))
            {
                var repository = new UserRepository(assertContext);

                var savedUser = await repository.GetByEmailAsync(email);

                Assert.NotNull(savedUser);
                Assert.Equal(email, savedUser!.Email);
                Assert.Equal(password, savedUser.Password);
            }
        }
        finally
        {
            await using var cleanupContext = new AuthDbContext(options);
            await cleanupContext.Database.EnsureDeletedAsync();
        }
        
    }

    [Fact]
    public async Task Should_Retrieve_User_ById()
    {
        var dbName = $"auth_test_{Guid.NewGuid()}";
        var options = CreateOptions(dbName);

        await using (var setupContext = new AuthDbContext(options))
        {
            await setupContext.Database.EnsureDeletedAsync();
            await setupContext.Database.EnsureCreatedAsync();
        }
        try
        {
            var email = Email.Create("john@doe.com");
            var password = Password.FromHash("hashed-password");

            var user = User.Create(email, password);

            await using (var context = new AuthDbContext(options))
            {
                var repository = new UserRepository(context);

                await repository.AddAsync(user);
                await context.SaveChangesAsync();
            }

            await using (var assertContext = new AuthDbContext(options))
            {
                var repository = new UserRepository(assertContext);

                var savedUser = await repository.GetByIdAsync(user.Id);

                Assert.NotNull(savedUser);
                Assert.Equal(email, savedUser!.Email);
                Assert.Equal(password, savedUser.Password);
            }
        }
        finally
        {
            await using var cleanupContext = new AuthDbContext(options);
            await cleanupContext.Database.EnsureDeletedAsync();
        }
    }

    [Fact]
    public async Task Should_Check_Existence_By_Email()
    {
        var dbName = $"auth_test_{Guid.NewGuid()}";
        var options = CreateOptions(dbName);

        await using (var setupContext = new AuthDbContext(options))
        {
            await setupContext.Database.EnsureDeletedAsync();
            await setupContext.Database.EnsureCreatedAsync();
        }

        try
        {
            var email = Email.Create("john@doe.com");
            var password = Password.FromHash("hashed-password");

            var user = User.Create(email, password);

            await using (var context = new AuthDbContext(options))
            {
                var repository = new UserRepository(context);

                await repository.AddAsync(user);
                await context.SaveChangesAsync();
            }

            await using (var assertContext = new AuthDbContext(options))
            {
                var repository = new UserRepository(assertContext);

                var savedUser = await repository.ExistsByEmailAsync(email);

                Assert.True(savedUser);
            }
        }
        finally
        {
            await using var cleanupContext = new AuthDbContext(options);
            await cleanupContext.Database.EnsureDeletedAsync();
        }
    }
}