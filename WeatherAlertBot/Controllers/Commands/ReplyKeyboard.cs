using Telegram.Bot.Types.ReplyMarkups;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Controllers.Commands
{
    public class ReplyKeyboard : IReplyKeyboard
    {
        public ReplyKeyboardMarkup GetOneTimeMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "/changelocation" },
                new KeyboardButton[] { "/changemorningtime" },
                new KeyboardButton[] { "/currentweather" }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetPermanentMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "/currentweather" },
                new KeyboardButton[] { "/settings" },
                new KeyboardButton[] { "/help" }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false
            };
        }

        public ReplyKeyboardMarkup GetBoolMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "YES" },
                new KeyboardButton[] { "NO" },
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false
            };
        }
    }
}
