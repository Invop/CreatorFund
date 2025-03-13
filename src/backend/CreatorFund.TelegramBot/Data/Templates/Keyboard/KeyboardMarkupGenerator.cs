using Telegram.Bot.Types.ReplyMarkups;

namespace CreatorFund.TelegramBot.Data.Templates.Keyboard;

public class KeyboardMarkupGenerator
{
    public InlineKeyboardMarkup GenerateInlineKeyboard()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("1.1"),
                InlineKeyboardButton.WithCallbackData("1.2"),
                InlineKeyboardButton.WithCallbackData("1.3")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("WithCallbackData", "CallbackData"),
                InlineKeyboardButton.WithUrl("WithUrl", "https://github.com/TelegramBots/Telegram.Bot")
            }
        });
    }

    public ReplyKeyboardMarkup GenerateReplyKeyboard()
    {
        return new ReplyKeyboardMarkup([
            new KeyboardButton[] { "1.1", "1.2", "1.3" },
            new KeyboardButton[] { "2.1", "2.2" }
        ])
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true,
        };
    }
}