namespace CreatorFund.TelegramBot.Configuration;

public class BotConfiguration
{
    public string BotToken { get; init; } = default!;
    public long AdminId { get; init; }
    public long ChatId { get; init; }
}
