using CreatorFund.TelegramBot.Abstract;

namespace CreatorFund.TelegramBot.Services;

// Compose Polling and ReceiverService implementations
public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService>(serviceProvider, logger);
