using Amazon.S3;
using FluentValidation;
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

    public static TBuilder AddRabbitMq<TBuilder>(this TBuilder builder, string connectionName)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddRabbitMqEventBus(connectionName);
        return builder;
    }

    public static TBuilder AddDatabase<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        //builder.AddNpgsqlDbContext<CreatorFundDbContext>();
        return builder;
    }

    public static TBuilder AddAws<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.Configure<AWSResources>(
            builder.Configuration.GetSection("AWS:Resources"));
        builder.Services.AddAWSService<IAmazonS3>();
        return builder;
    }
}

public class AWSResources
{
    public string? BucketName { get; set; }
}
