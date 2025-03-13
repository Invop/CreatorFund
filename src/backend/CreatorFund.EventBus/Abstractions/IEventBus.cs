namespace CreatorFund.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
    public Task PublishDelayedAsync(IntegrationEvent @event, TimeSpan delay);
}
