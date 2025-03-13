using CreatorFund.TelegramBot.Configuration;
using Telegram.Bot.Types;

namespace CreatorFund.TelegramBot.Services.Messaging;

public class MessageProcessor
{
    private readonly IMessageHandler _messageHandler;

    public MessageProcessor(IMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    public async Task<Message> ProcessMessageAsync(string messageText, Message msg)
    {
        var command = messageText.Trim().Split(' ')[0];
        return await (command switch
        {
            Commands.Start => _messageHandler.Start(msg),
            _ => Task.FromResult(msg)
        });
    }
}
