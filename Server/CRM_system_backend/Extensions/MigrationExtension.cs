using CRMSystem.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CRM_system_backend.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SystemDbContext>();
        dbContext.Database.Migrate();
    }
}