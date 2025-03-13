using CreatorFund.Application.Data;
using CreatorFund.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreatorFund.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static TBuilder AddDatabase<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.AddNpgsqlDbContext<CreatorFundDbContext>("demosdb");

        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CreatorFundDbContext>();

            // Retry logic to wait for database readiness
            WaitForDatabaseAsync(scope.ServiceProvider).GetAwaiter().GetResult();

            // Apply pending migrations
            dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
        }

        builder.Services.AddScoped<DemoService>();
        return builder;
    }

    private static async Task WaitForDatabaseAsync(IServiceProvider services, int retries = 5, int delayInSeconds = 5)
    {
        while (retries > 0)
        {
            try
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CreatorFundDbContext>();
                await dbContext.Database.CanConnectAsync();
                return; // Connection succeeded
            }
            catch
            {
                retries--;
                if (retries == 0)
                {
                    throw; // Rethrow after retries
                }

                await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));
            }
        }
    }
}
