using JoseApiRest.Infrastructure.Services.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JoseApiRest.Infrastructure;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        dbContext.Database.Migrate();
    }
}
