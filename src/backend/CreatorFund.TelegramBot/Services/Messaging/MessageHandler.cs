using CreatorFund.TelegramBot.Configuration;
using CreatorFund.TelegramBot.Data.Templates.ResponseMessages;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CreatorFund.TelegramBot.Services.Messaging;

public class MessageHandler(
    ITelegramBotClient bot,
    ILogger<UpdateHandler> logger,
    IOptions<BotConfiguration> botConfigOptions) : IMessageHandler
{
    public async Task<Message> Start(Message msg)
    {
        if (IsAdmin(msg))
        {
            // Admin-specific logic here
        }

        return await bot
            .SendMessage(
                msg.Chat, StartCommandResponseTemplate.Message,
                ParseMode.Html,
                replyMarkup: new ReplyKeyboardRemove());
    }

    private bool IsAdmin(Message message)
    {
        if (message?.From == null)
        {
            return false;
        }

        return message.From.Id == botConfigOptions.Value.AdminId;
    }
}
