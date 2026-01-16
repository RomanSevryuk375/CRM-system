// Ignore Spelling: Respawner

using CRM_system_backend;
using CRMSystem.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;

public class IntegrationTestFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:18")
        .Build();
    public Respawner Respawner { get; private set; } = default!;


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<SystemDbContext>));

            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<SystemDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var connection = new Npgsql.NpgsqlConnection(_dbContainer.GetConnectionString());
        await connection.OpenAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SystemDbContext>();
        await dbContext.Database.MigrateAsync();

        Respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new [] { "public" },
            TablesToIgnore = new Table[]
                {
                    "__EFMigrationsHistory",
                    "roles",
                    "order_statuses",
                    "order_priorities",
                    "bill_statuses",
                    "car_statuses",
                    "absence_types",
                    "expense_types",
                    "notification_statuses",
                    "notification_types",
                    "tax_types",
                    "work_in_order_statuses",
                    "work_proposal_statuses",
                    "specializations",
                    "payment_methods"
                }
        });
    }
    public string GetConnectionString() => _dbContainer.GetConnectionString();
    public new async Task DisposeAsync() => await _dbContainer.StopAsync();
}