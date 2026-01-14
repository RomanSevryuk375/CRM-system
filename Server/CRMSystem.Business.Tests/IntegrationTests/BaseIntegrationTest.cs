using CRMSystem.DataAccess;

namespace CRMSystem.Business.Tests.IntegrationTests;

public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly IntegrationTestFactory _factory;
    private readonly IServiceScope _scope;
    protected readonly SystemDbContext dbContext;
    protected readonly IServiceProvider serviceProvider;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        _factory = factory;
        _scope = factory.Services.CreateScope();
        serviceProvider = _scope.ServiceProvider;

        dbContext = serviceProvider.GetRequiredService<SystemDbContext>();
    }

    public async Task InitializeAsync()
    {
        using var connection = new Npgsql.NpgsqlConnection(_factory.GetConnectionString());
        await connection.OpenAsync();

        await _factory.Respawner.ResetAsync(connection);
    }
    public async Task DisposeAsync()
    {
        if (_scope is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
        else
        {
            _scope.Dispose();
        }
    }
}
