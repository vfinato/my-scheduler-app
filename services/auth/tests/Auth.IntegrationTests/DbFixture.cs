using Testcontainers.PostgreSql;

namespace Auth.IntegrationTests
{
    public class DbFixture : IAsyncLifetime
    {
        public PostgreSqlContainer postgreSqlContainer { get; } = new PostgreSqlBuilder()
        .WithImage("postgres:13")
        .WithCleanUp(true) // Garante que o container suma se o processo travar
        .Build();

        // Essa string contém a porta dinâmica e o banco padrão "postgres"
        public string BaseConnectionString => postgreSqlContainer.GetConnectionString();

        public async Task InitializeAsync() => await postgreSqlContainer.StartAsync();

        public async Task DisposeAsync() => await postgreSqlContainer.StopAsync();
 
    }
}