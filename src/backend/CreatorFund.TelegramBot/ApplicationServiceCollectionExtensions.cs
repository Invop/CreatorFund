namespace CreatorFund.TelegramBot;

public static class ApplicationServiceCollectionExtensions
{
    public static TBuilder AddRabbitMq<TBuilder>(this TBuilder builder, string connectionName)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddRabbitMqEventBus(connectionName);
        return builder;
    }
}
