using Telegram.Bot.Types;

namespace CreatorFund.TelegramBot.Services.Messaging;

public interface IMessageHandler
{
    Task<Message> Start(Message msg);
}
